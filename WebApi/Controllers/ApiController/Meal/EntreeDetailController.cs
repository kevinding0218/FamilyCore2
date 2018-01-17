using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Persistent.Meal;
using WebApi.Persistent.Shared;
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
        [HttpGet("{entreeDetailType}")]
        public async Task<IEnumerable<GridEntreeDetailResource>> GetEntreeDetails(string entreeDetailType)
        {
            var convertEntreeDetailType = TranslateEntreeDetailType(entreeDetailType);
            var entreeDetails = await this._entreeDetailRepository.GetEntreeDetails(convertEntreeDetailType);
            var gridResult = _mapper.Map<IEnumerable<EntreeDetail>, IEnumerable<GridEntreeDetailResource>>(entreeDetails);

            foreach (var gridEntreeDetail in gridResult)
            {
                var AddedByUserId = gridEntreeDetail.AddedById;
                var entreeDetailId = gridEntreeDetail.keyValuePairInfo.Id;

                gridEntreeDetail.AddedByUserName = await _userRepository.GetUserFullName(AddedByUserId);
                gridEntreeDetail.NumberOfEntreeIncluded = await _entreeDetailRepository.GetNumberOfEntreesWithEntreeDetail(entreeDetailId);
                gridEntreeDetail.EntreesIncluded = await _entreeRepository.GetEntreeInfoWithMeatId(entreeDetailId);

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

        private static string TranslateEntreeDetailType(string entreeType)
        {
            switch (entreeType.ToLower())
            {
                case "meat":
                    return EntreeDetailTypeEnum.Meat;
                case "vegetable":
                    return EntreeDetailTypeEnum.Vegetable;
                case "seafood":
                    return EntreeDetailTypeEnum.Seafood;
                case "ingredient":
                    return EntreeDetailTypeEnum.Ingredient;
                case "sauce":
                    return EntreeDetailTypeEnum.Sauce;
                default:
                    return string.Empty;
            }
        }
        #endregion

        #region  READ SINGLE OBJECT
        [HttpGet("id")]
        public async Task<IActionResult> GetEntreeDetail(int id)
        {
            var isExistedentreeDetail = await _entreeDetailRepository.GetEntreeDetail(id);
            if (isExistedentreeDetail == null)
                return NotFound();

            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(isExistedentreeDetail);

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
                var entreeDetailType = TranslateEntreeDetailType(newEntreeDetailResource.DetailType);
                var newentreeDetail = _mapper.Map<SaveEntreeDetailResource, EntreeDetail>(newEntreeDetailResource);
                newentreeDetail.AddedOn = DateTime.Now;
                if (!entreeDetailType.Equals(String.Empty))
                {
                    newentreeDetail.EntreeDetailTypeId = await _entreeDetailRepository.GetEntreeDetailTypeIdByType(entreeDetailType);
                }

                // Insert into database by using Domain Model
                _entreeDetailRepository.AddEntreeDetail(newentreeDetail);
                await _uow.CompleteAsync();

                newentreeDetail = await _entreeDetailRepository.GetEntreeDetail(newentreeDetail.Id);
                // Convert from Domain Model to View Model
                var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(newentreeDetail);

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
        public async Task<IActionResult> UpdateEntreeDetail(int id, [FromBody] SaveEntreeDetailResource SaveEntreeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isExistedentreeDetail = await _entreeDetailRepository.GetEntreeDetail(id);
            if (isExistedentreeDetail == null)
                return NotFound();

            if (await _entreeDetailRepository.IsDuplicateEntreeDetail(SaveEntreeResource.keyValuePairInfo.Name, SaveEntreeResource.keyValuePairInfo.Id))
            {
                ModelState.AddModelError("DuplicateEntreeDetail", SaveEntreeResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeDetailResource, EntreeDetail>(SaveEntreeResource, isExistedentreeDetail);
            isExistedentreeDetail.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            isExistedentreeDetail = await _entreeDetailRepository.GetEntreeDetail(isExistedentreeDetail.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(isExistedentreeDetail);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/entreeDetail/id
        public async Task<IActionResult> DeleteEntreeDetail(int id)
        {
            var existedEntreeDetail = await _entreeDetailRepository.GetEntreeDetail(id);
            if (existedEntreeDetail == null)
                return NotFound();

            _entreeDetailRepository.Remove(existedEntreeDetail);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}