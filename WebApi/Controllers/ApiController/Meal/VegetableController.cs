using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Persistent.Meal;
using WebApi.Persistent.Query;
using WebApi.Persistent.User;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.EntreeResource;
using WebApi.Resource.QueryResource;

namespace WebApi.Controllers.ApiController.Meal
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/vegetable")]
    public class VegetableController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IVegetableRepository _vegeRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IEntreeRepository _entreeRepository;

        public VegetableController(
            IMapper mapper,
            IVegetableRepository vegeRepository,
            IUserRepository userRepository,
            IEntreeRepository entreeRepository,
            IUnitOfWork uow)
        {
            this._userRepository = userRepository;
            this._entreeRepository = entreeRepository;
            this._uow = uow;
            this._vegeRepository = vegeRepository;
            this._mapper = mapper;
        }

        #region READ LIST OF OBJECTS
        [HttpGet]
        public async Task<QueryResultResource<GridEntreeDetailResource>> GetVegetables(VegetableQueryResource filterResource)
        {
            //var result = new QueryResultResource<GridEntreeComponentResource>();
            var filter = _mapper.Map<VegetableQueryResource, VegetableQuery>(filterResource);

            var queryResult = await _vegeRepository.GetVegetables(filter);
            //result.TotalItems = queryResult.TotalItems;
            var queryResultResource = _mapper.Map<QueryResult<EntreeDetail>, QueryResultResource<GridEntreeDetailResource>>(queryResult);

            #region  Apply additional fields to Result and apply sorting/paging
            var queryResultItemsQueryable = queryResultResource.Items.AsQueryable();

            foreach (var gridVegetable in queryResultItemsQueryable)
            {
                var AddedByUserId = gridVegetable.AddedById;
                var VegeId = gridVegetable.keyValuePairInfo.Id;

                gridVegetable.AddedByUserName = await _userRepository.GetUserFullName(AddedByUserId);
                gridVegetable.NumberOfEntreeIncluded = await _vegeRepository.GetNumberOfEntreesWithVege(VegeId);
                //gridVegetable.EntreesIncluded = await this._entreeRepository.GetEntreeInfoWithVegeId(VegeId);

                if (gridVegetable.EntreesIncluded != null && gridVegetable.EntreesIncluded.Count() > 0)
                {
                    foreach (var entreeInfo in gridVegetable.EntreesIncluded)
                    {
                        entreeInfo.EntreeDetailList = await this._entreeRepository.GetEntreeDetailWithEntreeId(entreeInfo.EntreeId);
                    }
                }
                //gridVegetable.SetEntrees(new List<string>(new string[] { "Entree 1", "Entree 2", "Entree 3" }));
            }

            var columnsMap = new Dictionary<string, Expression<Func<GridEntreeDetailResource, object>>>()
            {
                ["addedBy"] = gv => gv.AddedByUserName,
                ["entreesIncluded"] = gv => gv.NumberOfEntreeIncluded
            };

            queryResultItemsQueryable = queryResultItemsQueryable.ApplyOrdering(filter, columnsMap);
            queryResultResource.TotalItemList = queryResultItemsQueryable.ToList();
            queryResultItemsQueryable = queryResultItemsQueryable.ApplyPaging(filter);
            //ToListAsync and CountAsync only works for EF query which is like _context.Objects, etc
            //result.Items = queryResultItemsQueryable.ToList();
            queryResultResource.Items = queryResultItemsQueryable.ToList();
            #endregion

            return queryResultResource;
        }
        #endregion

        #region  READ SINGLE OBJECT
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVegetable(int id)
        {
            var isExistedVegetable = await _vegeRepository.GetVegetable(id);
            if (isExistedVegetable == null)
                return NotFound();

            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(isExistedVegetable);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateVegetable([FromBody] SaveEntreeDetailResource newVegetableResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.IsExistedUser(newVegetableResource.AddedById))
            {
                ModelState.AddModelError("NonExistedUser", "User Not Found!");
                return BadRequest(ModelState);
            }

            if (await _vegeRepository.IsDuplicateVegetable(newVegetableResource.keyValuePairInfo.Name))
            {
                ModelState.AddModelError("DuplicateVegetable", newVegetableResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            // Convert from View Model to Domain Model
            var newVegetable = _mapper.Map<SaveEntreeDetailResource, EntreeDetail>(newVegetableResource);
            newVegetable.AddedOn = DateTime.Now;
            newVegetable.EntreeDetailTypeId = 6;

            // Insert into database by using Domain Model
            _vegeRepository.AddVegetable(newVegetable);
            await _uow.CompleteAsync();

            newVegetable = await _vegeRepository.GetVegetable(newVegetable.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(newVegetable);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  UPDATE
        [HttpPut("{id}")] //api/vegetable/id
        public async Task<IActionResult> UpdateVegetable(int id, [FromBody] SaveEntreeDetailResource SaveEntreeComponentResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isExistedVegetable = await _vegeRepository.GetVegetable(id);
            if (isExistedVegetable == null)
                return NotFound();

            if (await _vegeRepository.IsDuplicateVegetable(SaveEntreeComponentResource.keyValuePairInfo.Name, SaveEntreeComponentResource.keyValuePairInfo.Id))
            {
                ModelState.AddModelError("DuplicateVegetable", SaveEntreeComponentResource.keyValuePairInfo.Name + " already existed!");
                return BadRequest(ModelState);
            }

            //var test = await _vegeRepository.GetVegetableWithSameName(SaveEntreeComponentResource.keyValuePairInfo.Name);
            //if (test != null && test.Id != id)
            //{
            //    ModelState.AddModelError("DuplicateVegetable", SaveEntreeComponentResource.keyValuePairInfo.Name + " already existed!");
            //    return BadRequest(ModelState);
            //}

            // Convert from View Model to Domain Model
            _mapper.Map<SaveEntreeDetailResource, EntreeDetail>(SaveEntreeComponentResource, isExistedVegetable);
            isExistedVegetable.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            await _uow.CompleteAsync();

            // Fetch complete object from database
            isExistedVegetable = await _vegeRepository.GetVegetable(isExistedVegetable.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<EntreeDetail, SaveEntreeDetailResource>(isExistedVegetable);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  DELETE
        [HttpDelete("{id}")] //api/vegetable/id
        public async Task<IActionResult> DeleteVegetable(int id)
        {
            var existedVegetable = await _vegeRepository.GetVegetable(id);
            if (existedVegetable == null)
                return NotFound();

            _vegeRepository.Remove(existedVegetable);
            await _uow.CompleteAsync();

            return Ok(id);
        }
        #endregion
    }
}