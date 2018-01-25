using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCafe.Infrastructure.Services.Abstract;
using Microsoft.Extensions.Logging;
using eCafe.Infrastructure.Models;
using eCafe.WebUI.Responses;
using System.Net;
using eCafe.Infrastructure.Constants;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Services.PropertyMapping;
using Microsoft.AspNetCore.Cors;

namespace eCafe.WebUI.Controllers
{
    [EnableCors("All")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Produces(ApiMessages.ApplicationJson)]
    [Route("api/MenuDetail")]
    public class MenuDetailController : BaseController
    {
        private readonly IMenuDetailService _menuDetailService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuDetailService"></param>
        /// <param name="typeHelperService"></param>
        /// <param name="logger"></param>
        /// <param name="loggerRepository"></param>
        /// <param name="urlHelper"></param>
        public MenuDetailController(IMenuDetailService menuDetailService,
           ITypeHelperService typeHelperService,
           ILogger<MenuDetailController> logger,
           IUrlHelper urlHelper,
           ILoggerRepository loggerRepository) :
            base(typeHelperService, logger, urlHelper, loggerRepository)
        {
            _menuDetailService = menuDetailService;
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
        [Route("CusineDetail", Name = "GetMenuDetails")]
        public async Task<IActionResult> GetMenuDetails(Int32? pageSize = 100, Int32? pageNumber = 1, String name = null)
        {
            // throw new InvalidOperationException("This is an unhandled exception");
            var response = new ListModelResponse<MenuDetailDto>() as IListModelResponse<MenuDetailDto>;
            response.PageSize = (Int32)pageSize;
            response.PageNumber = (Int32)pageNumber;
            response.Model = await _menuDetailService.GetMenuDetailAsync(response.PageSize, response.PageNumber, name);
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("CusineDetail/{id}", Name = "GetMenuDetail")]
        public IActionResult GetMenuDetail(int id)
        {
            var response = CreateSingleModelResponse();
            // var entity = await _menuDetailService.GetMenuDetailAsync(id);
            var entity = _menuDetailService.GetMenuDetail(id);
            response.Model = entity;
            return response.ToHttpResponse();
        }        

        [HttpGet]
        [Route("CusineDetail/SubMenu/{id}", Name = "GetMenuDetailBySubMenu")]
        public async Task<IActionResult> GetMenuDetailBySubMenu(int id)
        {
            var response = new ListModelResponse<MenuDetailDto>() as IListModelResponse<MenuDetailDto>;
            response.Model = await _menuDetailService.GetMenuDetailByAsync(id); ;
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("CusineDetail", Name = "CreateMenuDetail")]
        [Produces(ApiMessages.ApplicationJson, Type = typeof(MenuDetailDto))]
        public async Task<IActionResult> CreateMenuDetail([FromBody]MenuDetailDto model)
        {
            var response = CreateSingleModelResponse();
            //*** check for model is valid or not if invalid returning bad request response
            if (model == null)
            {
                return response.ToErrorResponse(ApiMessages.InvalidRequest, HttpStatusCode.BadRequest);
            }
            // Check for duplicate Canonical text for the same name.
            if (await _menuDetailService.IsMenuDetailDuplicateAsync(model))
            {
                return response.ToErrorResponse(ApiMessages.DuplicateRecord, HttpStatusCode.Conflict);
            }
            model.Guid = Guid.NewGuid();
            model.IsActive = true;
            var entity = await _menuDetailService.CreateMenuDetailAsync(model);
            response.Model = entity;
            response.Message = entity.Id != 0 ? ApiMessages.SavedRecord : ApiMessages.SavedFailedRecord;
            return response.ToHttpResponse();
            //return CreatedAtRoute("GetAll", new { id = response.Model.Id }, response.Model);
        }

        [HttpPut]
        [Route("CusineDetail", Name = "UpdateMenuDetail")]
        [Produces(ApiMessages.ApplicationJson, Type = typeof(MenuDetailDto))]
        public async Task<IActionResult> UpdateMenuDetail([FromBody]MenuDetailDto value)
        {
            var response = CreateSingleModelResponse();
            var entity = await _menuDetailService.UpdateMenuDetailAsync(value);
            response.Model = entity;
            response.Message = entity.Id != 0 ? ApiMessages.UpdatedRecord : ApiMessages.UpdatedFailedRecord;
            return response.ToHttpResponse();
        }

        [HttpDelete]
        [Route("CusineDetail/{id}", Name = "DeleteMenuDetail")]
        public async Task<IActionResult> DeleteMenuDetail(int id)
        {
            var response = CreateSingleModelResponse();
            var entity = await _menuDetailService.DeleteMenuDetailAsync(id);
            response.Model = entity;
            response.Message = entity.Id != 0 ? ApiMessages.DeletedRecord : ApiMessages.DeletedFailedRecord;
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Search all menus
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CusineDetail/Search/{searchText}", Name = "SearchAllMenus")]
        public async Task<IActionResult> SearchAllMenus(string searchText)
        {
            var response = new ListModelResponse<MenuDetailDto>() as IListModelResponse<MenuDetailDto>;
            var entity = await _menuDetailService.SearchByMenuName(searchText);
            response.Model = entity;
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        private static ISingleModelResponse<MenuDetailDto> CreateSingleModelResponse()
        {
            return new SingleModelResponse<MenuDetailDto>() as ISingleModelResponse<MenuDetailDto>;
        }


    }
}