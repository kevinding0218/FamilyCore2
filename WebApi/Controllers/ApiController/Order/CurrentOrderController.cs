using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Persistent.Order.CurrentOrder;
using WebApi.Persistent.Utility;
using WebApi.Resource.Order;

namespace WebApi.Controllers.ApiController.Order
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/currentOrder")]
    public class CurrentOrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentOrderRepository _currentOrderRepository;

        public CurrentOrderController(
            IMapper mapper,
            ICurrentOrderRepository currentOrderRepository,
            IUnitOfWork uow
        )
        {
            this._uow = uow;
            this._mapper = mapper;
            this._currentOrderRepository = currentOrderRepository;
        }

        #region  READ SINGLE OBJECT
        //api/currentOrder/byId?orderId=a&includeMapping=b&includeEntree=c
        [HttpGet("byId")]
        public async Task<IActionResult> GetOrderByOrderId(int orderId, bool includeMapping, bool includeEntree)
        {
            var currentOrder = await this._currentOrderRepository.GetOrder(orderId, includeMapping: includeMapping, includeEntree: includeEntree);

            var saveCurrentOrder = _mapper.Map<DomainLibrary.Order.Order, SaveInitialOrder>(currentOrder);
            // Return view Model
            return Ok(saveCurrentOrder);
        }

        //api/currentOrder/byCurrentDate?currentDateStr=a
        [HttpGet("byCurrentDate")]
        public async Task<IActionResult> GetOrderByCurrentDate(string currentDateStr)
        {

            var currentDate = currentDateStr.ToDateTimeFromFormat();
            var currentOrder = await this._currentOrderRepository.GetOrderByCurrentDate(currentDate);

            var saveCurrentOrder = _mapper.Map<DomainLibrary.Order.Order, SaveInitialOrder>(currentOrder);
            // Return view Model
            return Ok(saveCurrentOrder);
        }

        //api/currentOrder/currentWeekOrderPrepare
        [HttpGet("currentWeekOrderPrepare")]
        public async Task<OrderProcessInfo> GetCurrentWeekOrderPrepare()
        {
            var currentOrder = await this._currentOrderRepository.GetOrderByCurrentDate(DateTime.Now);
            var orderProcessInfo = _mapper.Map<DomainLibrary.Order.Order, OrderProcessInfo>(currentOrder);
            orderProcessInfo.EntreeInfoList = await _currentOrderRepository.GetCurrentWeekOrderPrepare(DateTime.Now, DateTime.Now);

            return orderProcessInfo;
        }

        //api/currentOrder/currentWeekOrderEntreeDetails
        [HttpGet("currentWeekOrderEntreeDetails")]
        public async Task<List<OrderEntreeDetailInfo>> GetCurrentWeekOrderEntreeDetails()
        {
            return await this._currentOrderRepository.GetCurrentWeekOrderEntreeDetails(DateTime.Now, DateTime.Now);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateCurrentOrder([FromBody] SaveInitialOrder newSaveOrder)
        {
            // Convert from View Model to Domain Model
            var newOrder = _mapper.Map<SaveInitialOrder, DomainLibrary.Order.Order>(newSaveOrder);
            newOrder.AddedOn = DateTime.Now;

            // Insert into database by using Domain Model
            _currentOrderRepository.AddOrder(newOrder);
            await _uow.CompleteAsync();

            newOrder = await _currentOrderRepository.GetOrder(newOrder.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<DomainLibrary.Order.Order, SaveInitialOrder>(newOrder);

            // Return view Model
            return Ok(result);
        }
        #endregion

        #region  UPDATE
        [HttpPut("updateInitialOrder/{id}")] //api/currentOrder/updateInitialOrder/id
        public async Task<IActionResult> UpdateInitialOrder(int id, [FromBody] SaveInitialOrder saveCurrentOrder)
        {
            var existedOrderFromDB = await _currentOrderRepository.GetOrder(saveCurrentOrder.Id);
            if (existedOrderFromDB == null)
                return NotFound();

            // Convert from View Model to Domain Model
            _mapper.Map<SaveInitialOrder, DomainLibrary.Order.Order>(saveCurrentOrder, existedOrderFromDB);
            existedOrderFromDB.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }

            // Fetch complete object from database
            existedOrderFromDB = await _currentOrderRepository.GetOrder(existedOrderFromDB.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<DomainLibrary.Order.Order, SaveInitialOrder>(existedOrderFromDB);

            // Return view Model
            return Ok(result);
        }

        [HttpPut("updateProcessingOrder/{id}")] //api/currentOrder/updateProcessingOrder/id
        public async Task<IActionResult> UpdateProcessingOrder(int id, [FromBody] OrderProcessInfo processOrder)
        {
            var existedOrderFromDB = await _currentOrderRepository.GetOrder(processOrder.Id);
            if (existedOrderFromDB == null)
                return NotFound();

            // Convert from View Model to Domain Model
            _mapper.Map<OrderProcessInfo, DomainLibrary.Order.Order>(processOrder, existedOrderFromDB);
            existedOrderFromDB.LastUpdatedByOn = DateTime.Now;

            // Insert into database by using Domain Model
            try
            {
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.StackTrace);
                return BadRequest(ModelState);
            }

            // Fetch complete object from database
            existedOrderFromDB = await _currentOrderRepository.GetOrder(existedOrderFromDB.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<DomainLibrary.Order.Order, SaveInitialOrder>(existedOrderFromDB);

            // Return view Model
            return Ok(result);
        }
        #endregion
    }
}