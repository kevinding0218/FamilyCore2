using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Persistent.Meal.EntreeRepo;
using WebApi.Persistent.Meal.MeatRepo;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.EntreeResource;

namespace WebApi.Controllers.ApiController.Meal
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/meat")]
    public class MeatController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IMeatRepository _meatRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IEntreeRepository _entreeRepository;

        public MeatController(
            IMapper mapper,
            IMeatRepository meatRepository,
            IUserRepository userRepository,
            IEntreeRepository entreeRepository,
            IUnitOfWork uow
        )
        {
            this._userRepository = userRepository;
            this._entreeRepository = entreeRepository;
            this._uow = uow;
            this._meatRepository = meatRepository;
            this._mapper = mapper;
        }

        #region READ LIST OF OBJECTS
        [HttpGet]
        public async Task<IEnumerable<GridEntreeResource>> GetMeats()
        {
            var meats = await this._meatRepository.GetMeats();
            var gridResult = _mapper.Map<IEnumerable<EntreeDetail>, IEnumerable<GridEntreeResource>>(meats);

            foreach (var gridMeat in gridResult)
            {
                var AddedByUserId = gridMeat.AddedByUserId;
                var MeatId = gridMeat.keyValuePairInfo.Id;

                gridMeat.AddedByUserName = await _userRepository.GetUserFullName(AddedByUserId);
                gridMeat.NumberOfEntreeIncluded = await _meatRepository.GetNumberOfEntreesWithMeat(MeatId);
                gridMeat.EntreesIncluded = await _entreeRepository.GetEntreeInfoWithMeatId(MeatId);

                if (gridMeat.EntreesIncluded != null && gridMeat.EntreesIncluded.Count() > 0)
                {
                    foreach (var entreeInfo in gridMeat.EntreesIncluded)
                    {
                        entreeInfo.EntreeDetailList = await this._entreeRepository.GetEntreeDetailWithEntreeId(entreeInfo.EntreeId);
                    }
                }
            }

            return gridResult;
        }
        #endregion

        #region  READ SINGLE OBJECT
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeat(int id)
        {
            var isExistedMeat = await _meatRepository.GetMeat(id);
            if (isExistedMeat == null)
                return NotFound();

            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeResource>(isExistedMeat);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateMeat([FromBody] SaveEntreeResource newMeatResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.IsExistedUser(newMeatResource.AddedByUserId))
            {
                ModelState.AddModelError("NonExistedUser", "User Not Found!");
                return BadRequest(ModelState);
            }

            if (await _meatRepository.IsDuplicateMeat(newMeatResource.keyValuePairInfo.Name))
            {
                ModelState.AddModelError("DuplicateMeat", newMeatResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            var newMeat = _mapper.Map<SaveEntreeResource, EntreeDetail>(newMeatResource);
            newMeat.AddedOn = DateTime.Now;

            // Insert into database by using Domain Model
            _meatRepository.AddMeat(newMeat);
            await _uow.CompleteAsync();

            newMeat = await _meatRepository.GetMeat(newMeat.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeResource>(newMeat);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  UPDATE
        [HttpPut("{id}")] //api/meat/id
        public async Task<IActionResult> UpdateMeat(int id, [FromBody] SaveEntreeResource SaveEntreeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isExistedMeat = await _meatRepository.GetMeat(id);
            if (isExistedMeat == null)
                return NotFound();

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeResource, EntreeDetail>(SaveEntreeResource, isExistedMeat);
            isExistedMeat.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            isExistedMeat = await _meatRepository.GetMeat(isExistedMeat.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeResource>(isExistedMeat);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/Meat/id
        public async Task<IActionResult> DeleteMeat(int id)
        {
            var existedMeat = await _meatRepository.GetMeat(id);
            if (existedMeat == null)
                return NotFound();

            _meatRepository.Remove(existedMeat);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}