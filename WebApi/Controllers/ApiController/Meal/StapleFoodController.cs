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
    [Route("/api/stapleFood")]
    public class StapleFoodController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IStapleFoodRepository _stapleFoodRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public StapleFoodController(
            IMapper mapper,
            IStapleFoodRepository StapleFoodRepository,
            IUserRepository userRepository,
            IEntreeRepository entreeRepository,
            IUnitOfWork uow
        )
        {
            this._userRepository = userRepository;
            this._uow = uow;
            this._stapleFoodRepository = StapleFoodRepository;
            this._mapper = mapper;
        }

        #region READ LIST OF OBJECTS
        [HttpGet]
        public async Task<IEnumerable<GridEntreeDetailResource>> GetStapleFoods()
        {
            var StapleFoods = await this._stapleFoodRepository.GetStapleFoods();
            var gridResult = _mapper.Map<IEnumerable<StapleFood>, IEnumerable<GridEntreeDetailResource>>(StapleFoods);

            foreach (var gridStapleFood in gridResult)
            {
                var AddedByUserId = gridStapleFood.AddedById;
                var StapleFoodId = gridStapleFood.keyValuePairInfo.Id;

                gridStapleFood.AddedByUserName = await _userRepository.GetUserFullName(AddedByUserId);
                gridStapleFood.NumberOfEntreeIncluded = await _stapleFoodRepository.GetNumberOfEntreesWithStapleFood(StapleFoodId);
                gridStapleFood.EntreesIncluded = await _stapleFoodRepository.GetEntreeInfoWithStapleFoodId(StapleFoodId);
            }

            return gridResult;
        }
        #endregion

        #region  READ SINGLE OBJECT
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStapleFood(int id)
        {
            var existedStapleFoodFromDB = await _stapleFoodRepository.GetStapleFood(id);
            if (existedStapleFoodFromDB == null)
                return NotFound();

            // Convert from Domain Model to View Model
            var result = _mapper.Map<StapleFood, SaveEntreeDetailResource>(existedStapleFoodFromDB);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateStapleFood([FromBody] SaveEntreeDetailResource newStapleFoodResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.IsExistedUser(newStapleFoodResource.AddedById))
            {
                ModelState.AddModelError("NonExistedUser", "User Not Found!");
                return BadRequest(ModelState);
            }

            if (await _stapleFoodRepository.IsDuplicateStapleFood(newStapleFoodResource.keyValuePairInfo.Name))
            {
                ModelState.AddModelError("DuplicateStapleFood", newStapleFoodResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            var newStapleFood = _mapper.Map<SaveEntreeDetailResource, StapleFood>(newStapleFoodResource);
            newStapleFood.AddedOn = DateTime.Now;

            // Insert into database by using Domain Model
            _stapleFoodRepository.AddStapleFood(newStapleFood);
            await _uow.CompleteAsync();

            newStapleFood = await _stapleFoodRepository.GetStapleFood(newStapleFood.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<StapleFood, SaveEntreeDetailResource>(newStapleFood);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  UPDATE
        [HttpPut("{id}")] //api/StapleFood/id
        public async Task<IActionResult> UpdateStapleFood(int id, [FromBody] SaveEntreeDetailResource SaveEntreeDetailResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existedStapleFoodFromDB = await _stapleFoodRepository.GetStapleFood(id);
            if (existedStapleFoodFromDB == null)
                return NotFound();

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeDetailResource, StapleFood>(SaveEntreeDetailResource, existedStapleFoodFromDB);
            existedStapleFoodFromDB.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            existedStapleFoodFromDB = await _stapleFoodRepository.GetStapleFood(existedStapleFoodFromDB.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<StapleFood, SaveEntreeDetailResource>(existedStapleFoodFromDB);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/StapleFood/id
        public async Task<IActionResult> DeleteStapleFood(int id)
        {
            var existedStapleFoodFromDB = await _stapleFoodRepository.GetStapleFood(id);
            if (existedStapleFoodFromDB == null)
                return NotFound();

            _stapleFoodRepository.Remove(existedStapleFoodFromDB);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}