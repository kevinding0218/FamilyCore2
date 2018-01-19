using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Persistent.Meal;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Controllers.ApiController.Meal
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/entreeDetail")]
    public class EntreeDetailController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IEntreeDetailRepository _entreeDetailRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IEntreeRepository _entreeRepository;

        public EntreeDetailController(
            IMapper mapper,
            IEntreeDetailRepository entreeDetailRepository,
            IUserRepository userRepository,
            IEntreeRepository entreeRepository,
            IUnitOfWork uow
        )
        {
            this._userRepository = userRepository;
            this._entreeRepository = entreeRepository;
            this._uow = uow;
            this._entreeDetailRepository = entreeDetailRepository;
            this._mapper = mapper;
        }

        #region READ LIST OF OBJECTS
        [HttpGet("{entreeDetailType}")] ///api/entreeDetail/entreeDetailType
        public async Task<IEnumerable<GridEntreeDetailResource>> GetEntreeDetails(string entreeDetailType)
        {
            var entreeDetails = await this._entreeDetailRepository.GetEntreeDetails(entreeDetailType);
            var gridResult = _mapper.Map<IEnumerable<EntreeDetail>, IEnumerable<GridEntreeDetailResource>>(entreeDetails);

            foreach (var gridEntreeDetail in gridResult)
            {
                var AddedByUserId = gridEntreeDetail.AddedById;
                var entreeDetailId = gridEntreeDetail.keyValuePairInfo.Id;

                gridEntreeDetail.AddedByUserName = await _userRepository.GetUserFullName(AddedByUserId);
                gridEntreeDetail.NumberOfEntreeIncluded = await _entreeDetailRepository.GetNumberOfEntreesWithEntreeDetail(entreeDetailId);
                gridEntreeDetail.EntreesIncluded = await _entreeDetailRepository.GetEntreeInfoWithEntreeDetailId(entreeDetailId);

                if (gridEntreeDetail.EntreesIncluded != null && gridEntreeDetail.EntreesIncluded.Count() > 0)
                {
                    foreach (var entreeInfo in gridEntreeDetail.EntreesIncluded)
                    {
                        entreeInfo.EntreeDetailList = await this._entreeRepository.GetEntreeDetailWithEntreeId(entreeInfo.EntreeId);
                    }
                }
            }

            return gridResult;
        }
        #endregion

        #region  READ SINGLE OBJECT
        [HttpGet("id")] ///api/entreeDetail/id?id=a
        public async Task<IActionResult> GetEntreeDetail(int id)
        {
            var existedEntreeDetailFromDB = await _entreeDetailRepository.GetEntreeDetail(id);
            if (existedEntreeDetailFromDB == null)
                return NotFound();

            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(existedEntreeDetailFromDB);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateEntreeDetail([FromBody] SaveEntreeDetailResource newEntreeDetailResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.IsExistedUser(newEntreeDetailResource.AddedById))
            {
                ModelState.AddModelError("NonExistedUser", "User Not Found!");
                return BadRequest(ModelState);
            }

            if (await _entreeDetailRepository.IsDuplicateEntreeDetail(newEntreeDetailResource.keyValuePairInfo.Name))
            {
                ModelState.AddModelError("DuplicateEntreeDetail", newEntreeDetailResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            try
            {
                // Convert from View Model to Domain Model 
                var newEntreeDetail = _mapper.Map<SaveEntreeDetailResource, EntreeDetail>(newEntreeDetailResource);
                newEntreeDetail.AddedOn = DateTime.Now;
                if (!newEntreeDetailResource.DetailType.Equals(String.Empty))
                {
                    newEntreeDetail.EntreeDetailTypeId = await _entreeDetailRepository
                                                                    .GetEntreeDetailTypeIdByType(newEntreeDetailResource.DetailType);
                }

                // Insert into database by using Domain Model
                _entreeDetailRepository.AddEntreeDetail(newEntreeDetail);
                await _uow.CompleteAsync();

                newEntreeDetail = await _entreeDetailRepository.GetEntreeDetail(newEntreeDetail.Id);
                // Convert from Domain Model to View Model
                var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(newEntreeDetail);

                // Return view Model
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region  UPDATE
        [HttpPut("{id}")] //api/entreeDetail/id
        public async Task<IActionResult> UpdateEntreeDetail(int id, [FromBody] SaveEntreeDetailResource SaveEntreeDetailResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existedEntreeDetailFromDB = await _entreeDetailRepository.GetEntreeDetail(id);
            if (existedEntreeDetailFromDB == null)
                return NotFound();

            if (await _entreeDetailRepository.IsDuplicateEntreeDetail(SaveEntreeDetailResource.keyValuePairInfo.Name, SaveEntreeDetailResource.keyValuePairInfo.Id))
            {
                ModelState.AddModelError("DuplicateEntreeDetail", SaveEntreeDetailResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeDetailResource, EntreeDetail>(SaveEntreeDetailResource, existedEntreeDetailFromDB);
            existedEntreeDetailFromDB.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            existedEntreeDetailFromDB = await _entreeDetailRepository.GetEntreeDetail(existedEntreeDetailFromDB.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(existedEntreeDetailFromDB);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/entreeDetail/id
        public async Task<IActionResult> DeleteEntreeDetail(int id)
        {
            var existedEntreeDetailFromDB = await _entreeDetailRepository.GetEntreeDetail(id);
            if (existedEntreeDetailFromDB == null)
                return NotFound();

            _entreeDetailRepository.Remove(existedEntreeDetailFromDB);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}