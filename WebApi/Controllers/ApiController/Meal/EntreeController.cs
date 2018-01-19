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
            var isExistedentree = await _entreeRepository.GetEntree(id);
            if (isExistedentree == null)
                return NotFound();

            var result = _mapper.Map<Entree, SaveEntreeResource>(isExistedentree);

            if (isExistedentree.MappingDetailsWithCurrentEntree != null && isExistedentree.MappingDetailsWithCurrentEntree.Count > 0)
            {
                foreach (var esds in isExistedentree.MappingDetailsWithCurrentEntree)
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
            var newentreeDetail = _mapper.Map<SaveEntreeResource, Entree>(newEntreeResource);
            newentreeDetail.AddedOn = DateTime.Now;

            // Insert into database by using Domain Model
            _entreeRepository.AddEntree(newentreeDetail);
            await _uow.CompleteAsync();

            newentreeDetail = await _entreeRepository.GetEntree(newentreeDetail.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<Entree, SaveEntreeDetailResource>(newentreeDetail);

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

            var isExistedDetail = await _entreeRepository.GetEntree(id);
            if (isExistedDetail == null)
                return NotFound();

            if (await _entreeRepository.IsDuplicateEntree(SaveEntreeResource.Name, SaveEntreeResource.Id))
            {
                ModelState.AddModelError("DuplicateEntree", SaveEntreeResource.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeResource, Entree>(SaveEntreeResource, isExistedDetail);
            isExistedDetail.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            isExistedDetail = await _entreeRepository.GetEntree(isExistedDetail.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<Entree, SaveEntreeResource>(isExistedDetail);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/entreeDetail/id
        public async Task<IActionResult> DeleteEntree(int id)
        {
            var existedEntree = await _entreeRepository.GetEntree(id, includeRelated: false);
            if (existedEntree == null)
                return NotFound();

            _entreeRepository.Remove(existedEntree);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}