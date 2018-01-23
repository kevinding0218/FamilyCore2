using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private readonly ICurrentOrder _currentOrderRepository;

        public CurrentOrderController(
            IMapper mapper,
            ICurrentOrder currentOrderRepository,
            IUnitOfWork uow
        )
        {
            this._uow = uow;
            this._mapper = mapper;
            this._currentOrderRepository = currentOrderRepository;
        }

        #region  READ SINGLE OBJECT
        [HttpGet("{currentDateStr}")]
        public async Task<IActionResult> GetOrderByCurrentDate(string currentDateStr)
        {
            //var currentDate = currentDateStr.ToDateTimeFromFormat();
            var currentOrder = await this._currentOrderRepository.GetOrderByCurrentDate(ToDateTimeFromFormat(currentDateStr));

            var saveCurrentOrder = _mapper.Map<DomainLibrary.Order.Order, SaveCurrentOrder>(currentOrder);
            // Return view Model
            return Ok(saveCurrentOrder);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> CreateCurrentOrder([FromBody] SaveCurrentOrder newSaveOrder)
        {
            // Convert from View Model to Domain Model
            var newOrder = _mapper.Map<SaveCurrentOrder, DomainLibrary.Order.Order>(newSaveOrder);
            newOrder.AddedOn = DateTime.Now;

            // Insert into database by using Domain Model
            _currentOrderRepository.AddOrder(newOrder);
            await _uow.CompleteAsync();

            newOrder = await _currentOrderRepository.GetOrder(newOrder.Id);
            // Convert from Domain Model to View Model
            var result = _mapper.Map<DomainLibrary.Order.Order, SaveCurrentOrder>(newOrder);

            // Return view Model
            return Ok(result);
        }
        #endregion

        private DateTime ToDateTimeFromFormat(string dateString)
        {
            Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}Z$");
            if (!r.IsMatch(dateString) || dateString == null)
            {
                //throw new FormatException(
                //    string.Format("{0} is not the correct format. Should be yyyyMMddThhmmZ", dateString));
                return DateTime.Now;
            }

            return DateTime.ParseExact(dateString, "yyyyMMddThhmmZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }
    }
}