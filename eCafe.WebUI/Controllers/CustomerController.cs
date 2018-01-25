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
using eCafe.WebUI.Responses;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Constants;
using eCafe.Core.Entities;
using System.Net;

namespace eCafe.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : BaseController
    {
        private ICustomerService _customerService;
        
        public CustomerController(
            ICustomerService customerService,
            ITypeHelperService typeHelperService,
            ILogger<BaseController> logger,
            IUrlHelper urlHelper,
            ILoggerRepository loggingRepository)
            : base(typeHelperService, logger, urlHelper, loggingRepository)
        {
            _customerService = customerService;
            base.logger.LogInformation($"{this.GetType().Name} created");

        }

        [HttpGet]
        [Route("CustomerList", Name = "GetCustomerDetails")]
        public async Task<IActionResult> GetCustomerDetails(Int32? pageSize = 10, Int32? pageNumber = 1, String name = null)
        {
            var response = new ListModelResponse<CustomerDto>() as IListModelResponse<CustomerDto>;
            response.PageSize = (Int32)pageSize;
            response.PageNumber = (Int32)pageNumber;
            response.Model = await _customerService.GetCustomerAsync(response.PageSize, response.PageNumber, name);
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("Customer/{id}", Name = "GetCustomerBy")]
        public async Task<IActionResult> GetCustomerBy(int id)
        {
            var response = new SingleModelResponse<CustomerDto>() as ISingleModelResponse<CustomerDto>;
            try
            {
                var entity = await _customerService
                            .GetCustomerAsync(id);

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
        [Route("Create", Name = "CreateCustomer")]
        [Produces("application/json", Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer([FromBody]CustomerDto model)
        {
            var response = CreateSingleModelResponse();
            //*** check for model is valid or not if invalid returning bad request response
            if (model == null)
            {
                return response.ToErrorResponse("Invalid Request", HttpStatusCode.BadRequest);
            }
            // Check for duplicate Canonical text for the same name.
            if (await _customerService.IsCustomerDuplicateAsync(model))
            {
                return response.ToErrorResponse("Record already exits in the system", HttpStatusCode.Conflict);
            }
            model.Guid = Guid.NewGuid();
            model.IsActive = true;
            var entity = await _customerService.CreateCustomerAsync(model);
            response.Model = entity;
            response.Message = "The data was saved successfully";
            return CreatedAtRoute("GetAll", new { id = response.Model.Id }, response.Model);
        }

        [HttpPut]
        [Route("Update", Name = "UpdateCustomer")]
        [Produces("application/json", Type = typeof(CustomerDto))]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerDto value)
        {
            var response = CreateSingleModelResponse();
            var entity = await _customerService.UpdateCustomerAsync(value);
            response.Model = entity;
            response.Message = entity.Id != 0 ? "The record was updated successfully" : "The record was not updated";
            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var response = CreateSingleModelResponse();
            var entity = await _customerService.DeleteCustomerAsync(id);
            response.Model = entity;
            response.Message = "The record was deleted successfully";
            return response.ToHttpResponse();
        }

        private static ISingleModelResponse<CustomerDto> CreateSingleModelResponse()
        {
            return new SingleModelResponse<CustomerDto>() as ISingleModelResponse<CustomerDto>;
        }
    }
}