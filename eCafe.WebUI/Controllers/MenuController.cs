using eCafe.WebUI.Services.ApiGetAll;
using eCafe.WebUI.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Net;
using eCafe.Infrastructure.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Services.PropertyMapping;
using eCafe.WebUI.Common;
using Microsoft.AspNetCore.Cors;
using eCafe.Core.Entities;
using eCafe.Infrastructure.Constants;

namespace eCafe.WebUI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors("All")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Produces("application/json")]
    [Route("api/Menu")]
    public class MenuController : BaseController
    {
        private readonly IMenuGet _menuGetService;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMenuService _menuService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuGetSevice"></param>
        /// <param name="propertyMappingService"></param>
        /// <param name="typeHelperService"></param>
        /// <param name="logger"></param>
        /// <param name="urlHelper"></param>
        public MenuController(IMenuService menuService,
           IMenuGet menuGetSevice,
           IPropertyMappingService propertyMappingService,
           ITypeHelperService typeHelperService,
           ILogger<MenuController> logger,
           IUrlHelper urlHelper,
           ILoggerRepository loggerRepository) :
            base(typeHelperService, logger, urlHelper, loggerRepository)
        {
            _menuService = menuService;
            _menuGetService = menuGetSevice;
            _propertyMappingService = propertyMappingService;
            _loggingRepository = loggerRepository;
            base.logger.LogInformation($"{this.GetType().Name} created");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SubCusine", Name = "GetMenus")]
        public async Task<IActionResult> GetMenus(Int32? pageSize = 100, Int32? pageNumber = 1, String name = null)
        {
            var response = new ListModelResponse<MenuDto>() as IListModelResponse<MenuDto>; ;
            response.PageSize = (Int32)pageSize;
            response.PageNumber = (Int32)pageNumber;
            response.Model = await _menuService.GetMenuAsync(response.PageSize, response.PageNumber, name);
            response.Message = $"Total of records: {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("SubCusine/WithCusineDetailCount/", Name = "GetAllSubMenuWithCount")]
        public async Task<IActionResult> GetAllSubMenuWithCount()
        {
            var response = new ListModelResponse<SubMenuResultSet>() as IListModelResponse<SubMenuResultSet>;
            var entity = await _menuService.GetSubMenuWithSubMenuDetailCount();
            response.Model = entity;
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("SelectSubCuisine/{id}", Name = "GetSelectedSubCuisines")]
        public async Task<IActionResult> GetSelectedSubCuisines(int id)
        {
            var response = new ListModelResponse<SelectOption>() as IListModelResponse<SelectOption>;
            response.PageSize = 0;
            response.PageNumber = 0;
            try
            {
                response.Model = _menuService.GetSelectOption(id);
                response.Message = $"Total of records: {response.Model.Count()}";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }
            return response.ToHttpResponse();
        }



        [HttpGet]
        [Route("SubCusine/{id}", Name = "GetSubCuisinesBy")]
        public async Task<IActionResult> GetSubCuisinesBy(int id)
        {
            var response = CreateSingleModelResponse();
            try
            {
                var entity = await _menuService
                            .GetMenuAsync(id);

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
        [Route("SubCusine", Name = "CreateMenu")]
        [Produces("application/json", Type = typeof(MenuDto))]
        public async Task<IActionResult> CreateMenu([FromBody]MenuDto model)
        {
            var response = CreateSingleModelResponse();
            //*** check for model is valid or not if invalid returning bad request response
            if (model == null)
            {
                return response.ToErrorResponse("Invalid Request", HttpStatusCode.BadRequest);
            }
            // Check for duplicate Canonical text for the same name.
            if (await _menuService.IsMenuDuplicateAsync(model))
            {
                return response.ToErrorResponse("Record already exits in the system", HttpStatusCode.Conflict);
            }
            model.Guid = Guid.NewGuid();
            model.IsActive = true;
            var entity = await _menuService.CreateMenuAsync(model);
            response.Model = entity;
            response.Message = "The data was saved successfully";
            return CreatedAtRoute("GetAll", new { id = response.Model.Id }, response.Model);
        }

        [HttpPut]
        [Route("SubCusine", Name = "UpdateMenu")]
        [Produces("application/json", Type = typeof(MenuDto))]
        public async Task<IActionResult> UpdateMenu([FromBody]MenuDto value)
        {
            var response = CreateSingleModelResponse();
            var entity = await _menuService.UpdateMenuAsync(value);
            response.Model = entity;
            response.Message = entity.Id != 0 ? "The record was updated successfully" : "The record was not updated";
            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("SubCusine/{id}", Name = "DeleteMenu")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var response = CreateSingleModelResponse();
            var entity = await _menuService.DeleteMenuAsync(id);
            response.Model = entity;
            response.Message = "The record was deleted successfully";
            return response.ToHttpResponse();
        }

        private static ISingleModelResponse<MenuDto> CreateSingleModelResponse()
        {
            return new SingleModelResponse<MenuDto>() as ISingleModelResponse<MenuDto>;
        }

    }
}