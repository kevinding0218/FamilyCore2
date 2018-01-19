using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Persistent.Meal;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Controllers.ApiController.Meal
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/entree")]
    public class EntreeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IEntreeRepository _entreeRepository;

        public EntreeController(
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
            this._mapper = mapper;
        }

        #region READ LIST OF OBJECTS
        //api/entree/group?splitBy=a&id=b
        [HttpGet("group")]
        public async Task<IEnumerable<EntreeInfoResource>> GetEntrees(string splitBy, int id)
        {
            var gridResult = await this._entreeRepository.GetSplitEntreesList(splitBy, id);

            foreach (var gridEntree in gridResult)
            {
                var entreeDetailId = gridEntree.EntreeId;
                gridEntree.EntreeDetailList = await this._entreeRepository.GetEntreeDetailWithEntreeId(entreeDetailId);
            }

            return gridResult;
        }
        #endregion

        #region  READ SINGLE OBJECT
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntree(int id)
        {
            var EntreeDetails = new List<EntreeDetailMappingResource>();
            var existedEntreeFromDB = await _entreeRepository.GetEntree(id);
            if (existedEntreeFromDB == null)
                return NotFound();

            var result = _mapper.Map<Entree, SaveEntreeResource>(existedEntreeFromDB);

            if (existedEntreeFromDB.MappingDetailsWithCurrentEntree != null && existedEntreeFromDB.MappingDetailsWithCurrentEntree.Count > 0)
            {
                foreach (var esds in existedEntreeFromDB.MappingDetailsWithCurrentEntree)
                {
                    EntreeDetailMappingResource map = new EntreeDetailMappingResource(esds.EntreeDetailId, esds.EntreeDetail.Name, esds.Quantity, esds.EntreeDetail.EntreeDetailType.DetailName);
                    EntreeDetails.Add(map);
                }

                result.EntreeDetails = EntreeDetails;
            }

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateEntree([FromBody] SaveEntreeResource newEntreeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.IsExistedUser(newEntreeResource.AddedById))
            {
                ModelState.AddModelError("NonExistedUser", "User Not Found!");
                return BadRequest(ModelState);
            }

            if (await _entreeRepository.IsDuplicateEntree(newEntreeResource.Name))
            {
                ModelState.AddModelError("DuplicateEntree", newEntreeResource.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            var newEntree = _mapper.Map<SaveEntreeResource, Entree>(newEntreeResource);
            newEntree.AddedOn = DateTime.Now;

            // Insert into database by using Domain Model
            _entreeRepository.AddEntree(newEntree);
            await _uow.CompleteAsync();

            newEntree = await _entreeRepository.GetEntree(newEntree.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<Entree, SaveEntreeResource>(newEntree);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  UPDATE
        [HttpPut("{id}")] //api/entree/id
        public async Task<IActionResult> UpdateEntree(int id, [FromBody] SaveEntreeResource SaveEntreeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existedEntreeFromDB = await _entreeRepository.GetEntree(id);
            if (existedEntreeFromDB == null)
                return NotFound();

            if (await _entreeRepository.IsDuplicateEntree(SaveEntreeResource.Name, SaveEntreeResource.Id))
            {
                ModelState.AddModelError("DuplicateEntree", SaveEntreeResource.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeResource, Entree>(SaveEntreeResource, existedEntreeFromDB);
            existedEntreeFromDB.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            existedEntreeFromDB = await _entreeRepository.GetEntree(existedEntreeFromDB.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<Entree, SaveEntreeResource>(existedEntreeFromDB);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/entreeDetail/id
        public async Task<IActionResult> DeleteEntree(int id)
        {
            var existedEntreeFromDB = await _entreeRepository.GetEntree(id, includeRelated: false);
            if (existedEntreeFromDB == null)
                return NotFound();

            _entreeRepository.Remove(existedEntreeFromDB);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}