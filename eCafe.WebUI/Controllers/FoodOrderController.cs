using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Services.PropertyMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Models;
using eCafe.WebUI.Responses;
using eCafe.Infrastructure.Constants;
using System.Net;

namespace eCafe.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderController")]
    public class FoodOrderController : BaseController
    {
        private IOrderService _orderService;

        public FoodOrderController(
            IOrderService orderService,
            ITypeHelperService typeHelperService,
            ILogger<BaseController> logger,
            IUrlHelper urlHelper,
            ILoggerRepository loggingRepository)
            : base(typeHelperService, logger, urlHelper, loggingRepository)
        {
            _orderService = orderService;
            base.logger.LogInformation($"{this.GetType().Name} created");
        }

        [HttpGet]
        [Route("OrderList", Name = "GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails(Int32? pageSize = 10, Int32? pageNumber = 1, String name = null)
        {
            var response = new ListModelResponse<OrderDto>() as IListModelResponse<OrderDto>;
            response.PageSize = (Int32)pageSize;
            response.PageNumber = (Int32)pageNumber;
            response.Model = await _orderService.GetOrderAsync(response.PageSize, response.PageNumber, name);
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Orders/{id}", Name = "GetOrderBy")]
        public async Task<IActionResult> GetCuisinesBy(int id)
        {
            var response = new SingleModelResponse<OrderDto>() as ISingleModelResponse<OrderDto>;
            try
            {
                var entity = await _orderService.GetOrderAsync(id);

                response.Model = entity;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("Create", Name = "CreateOrder")]
        [Produces(ApiMessages.ApplicationJson, Type = typeof(OrderDto))]
        public async Task<IActionResult> CreateOrder([FromBody]OrderDto model)
        {
            var response = CreateSingleModelResponse();
            //*** check for model is valid or not if invalid returning bad request response
            if (model == null)
            {
                return response.ToErrorResponse(ApiMessages.InvalidRequest, HttpStatusCode.BadRequest);
            }
            
            model.Guid = Guid.NewGuid();
            var entity = await _orderService.CreateOrderAsync(model);
            response.Model = entity;
            response.Message = entity.Id != 0 ? ApiMessages.SavedRecord : ApiMessages.SavedFailedRecord;
            return response.ToHttpResponse();
            //return CreatedAtRoute("GetAll", new { id = response.Model.Id }, response.Model);
        }

        private static ISingleModelResponse<OrderDto> CreateSingleModelResponse()
        {
            return new SingleModelResponse<OrderDto>() as ISingleModelResponse<OrderDto>;
        }
    }
}