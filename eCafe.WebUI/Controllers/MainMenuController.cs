using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCafe.WebUI.Responses;
using eCafe.Infrastructure.Common;
using System.Net;
using Microsoft.Extensions.Logging;
using eCafe.WebUI.Services.ApiGetAll;
using eCafe.Core.Entities;
using System.Collections.Generic;
using eCafe.WebUI.Common;
using Microsoft.AspNetCore.JsonPatch;
using eCafe.Infrastructure.Enums;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Mapper;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Services.PropertyMapping;
using eCafe.Infrastructure.Extensions;
using Microsoft.AspNetCore.Cors;

namespace eCafe.WebUI.Controllers
{
    /// <summary>
    /// Main Menu API
    /// </summary>
    [EnableCors("All")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Produces("application/json")]
    [Route("api/MainMenu")]
    public class MainMenuController : BaseController
    {
        private readonly IMainMenuRepository _mainMenuRepository;
        private readonly IMainMenuGet _mainMenuGetService;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMainMenuService _mainMenuService;


        /// <summary>
        /// Initialize Constructor
        /// </summary>
        /// <param name="mainMenuRepository"></param>
        /// <param name="mainMenuGetSevice"></param>
        /// <param name="propertyMappingService"></param>
        /// <param name="typeHelperService"></param>
        /// <param name="logger"></param>
        /// <param name="urlHelper"></param>
        public MainMenuController(IMainMenuRepository mainMenuRepository,
            IMainMenuGet mainMenuGetSevice,
            IPropertyMappingService propertyMappingService,
            ITypeHelperService typeHelperService,
            IMainMenuService mainMenuService,
            ILogger<MainMenuController> logger,
            IUrlHelper urlHelper,
            ILoggerRepository loggerRepository) :
            base(typeHelperService, logger, urlHelper, loggerRepository)
        {
            _mainMenuRepository = mainMenuRepository;
            _mainMenuGetService = mainMenuGetSevice;
            _mainMenuService = mainMenuService;
            base.logger.LogInformation($"{this.GetType().Name} created");
        }

        /// <summary>
        /// Return All Cuisines Version 1
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cuisine", Name = "GetCuisines")]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> GetCuisines(Int32? pageSize = 100, Int32? pageNumber = 1, String name = null)
        {
            var response = new ListModelResponse<MainMenuDto>() as IListModelResponse<MainMenuDto>;
            response.PageSize = (Int32)pageSize;
            response.PageNumber = (Int32)pageNumber;
            try
            {
                //response.Model = await _mainMenuRepository
                //            .GetAllAsync(response.PageSize, response.PageNumber, name)
                //            .Select(item => item.ToViewModel())
                //            .ToListAsync();
                // await System.Threading.Tasks.Task.Delay(5000);
                response.Model = await _mainMenuService.GetAsync(response.PageSize, response.PageNumber, name);
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
        [Route("SelectCuisine", Name = "GetSelectedCuisines")]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> GetSelectedCuisines()
        {
            var response = new ListModelResponse<SelectOption>() as IListModelResponse<SelectOption>;
            response.PageSize = 0;
            response.PageNumber = 0;
            try
            {
                response.Model = _mainMenuService.GetSelectOption();
                response.Message = $"Total of records: {response.Model.Count()}";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Return All Cuisines Version 2
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cuisine/V2", Name = "GetCuisinesV2")]
        [MapToApiVersion("1.1")]
        public IActionResult GetCuisinesV2(ResourceParameter parameters)
        {
            var response = new ListModelResponse<MainMenuDto>() as IListModelResponse<MainMenuDto>;
            //throw new InvalidOperationException("This is an unhandled exception");
            //throw new ECafeException("error occured");

            if (!_propertyMappingService.ValidMappingExistsFor<MainMenuDto, MainMenu>(parameters.OrderBy))
            {
                logger.LogError($"Invalid mapping requested in {this.GetType().Name} Method Name");
                return response.ToErrorResponse("Invalid mapping requested", HttpStatusCode.BadRequest);
            }

            if (!typeHelperService.TypeHasProperties<MainMenuDto>(parameters.Fields))
            {
                return response.ToErrorResponse("Invalid properties name requested", HttpStatusCode.BadRequest);
            }

            var results = _mainMenuGetService.GetAll(parameters);

            //***Create pagination header
            var paginationMetadata = ResourceUri<MainMenu>.CreatePaginationHeader(parameters, results, urlHelper, "GetCuisines");
            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            //***Mapping Entity to Dto
            var medicineTypes = AutoMapper.Mapper.Map<IEnumerable<MainMenuDto>>(results);
            response.ShapeModel = medicineTypes.ShapeData(parameters.Fields);
            response.Model = new List<MainMenuDto>();
            response.Message = $"Total of records: {response.ShapeModel.Count()}";
            response.PageSize = parameters.PageSize;
            response.PageNumber = parameters.PageNumber;
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Gets the MainMenu by specific Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cuisine/{id}", Name = "GetCuisinesBy")]
        public async Task<IActionResult> GetCuisinesBy(int id)
        {
            var response = new SingleModelResponse<MainMenuDto>() as ISingleModelResponse<MainMenuDto>;
            try
            {
                var entity = await _mainMenuRepository
                        .GetAsync(new MainMenu { Id = id });

                response.Model = entity.ToViewModel();
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Create the Cuisine.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Single response</returns>
        [HttpPost]
        [Route("Cuisine", Name = "CreateCuisine")]
        [Produces("application/json", Type = typeof(MainMenuDto))]

        public async Task<IActionResult> CreateCuisine([FromBody]MainMenuDto model)
        {
            var response = new SingleModelResponse<MainMenuDto>() as ISingleModelResponse<MainMenuDto>;

            //*** check for model is valid or not if invalid returning bad request response
            if (model == null)
            {
                response.IsError = true;
                response.ErrorMessage = "Invalid request";
                return new ObjectResult(response) { StatusCode = (Int32)HttpStatusCode.BadRequest };
            }

            // Check for duplicate Canonical text for the same name.
            var isDuplicate = await _mainMenuRepository.IsExistsAsync(x => x.Name.Equals(model.Name));
            if (isDuplicate)
            {
                // It's a duplicate.  Return an HTTP 409 Conflict error to let the client know.
                var original = await _mainMenuRepository
                                .GetAsync(new MainMenu { Id = model.Id });

                response.IsError = true;
                response.ErrorMessage = "Record already exits in the system";
                response.Model = original.ToViewModel();

                return new ObjectResult(response) { StatusCode = (Int32)HttpStatusCode.Conflict };
            }

            try
            {
                model.Guid = Guid.NewGuid();
                var entity = await _mainMenuRepository
                            .InsertAsync(model.ToEntity());

                response.Model = entity.ToViewModel();
                response.Message = "The data was saved successfully";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }

            return CreatedAtRoute("GetCuisines", new { id = response.Model.Id }, response.Model);
        }

        /// <summary>
        /// Updates an existing Cuisine
        /// </summary>
        /// <param name="value">MainMenu entry</param>
        /// <returns>Single response</returns>
        [HttpPut]
        [Route("Cuisine", Name = "UpdateCuisine")]
        [Produces("application/json", Type = typeof(MainMenuDto))]
        public async Task<IActionResult> UpdateCuisine([FromBody]MainMenuDto value)
        {
            var response = new SingleModelResponse<MainMenuDto>() as ISingleModelResponse<MainMenuDto>;
            try
            {
                var entity = await _mainMenuRepository
                         .UpdateAsync(value.ToEntity());

                if (entity != null)
                {
                    response.Model = entity.ToViewModel();
                    response.Message = "The record was updated successfully";
                }
                else
                {
                    response.Model = null;
                    response.Message = "The record was not updated";
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Updates an existing Cuisine partially
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mainMenuPatch"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Cuisine/{id}", Name = "PratiallyUpdate")]
        [Produces("application/json", Type = typeof(MainMenuDto))]
        public async Task<IActionResult> PratiallyUpdate(Int32 id, [FromBody] JsonPatchDocument<MainMenuDto> mainMenuPatch)
        {
            var response = new SingleModelResponse<MainMenuDto>() as ISingleModelResponse<MainMenuDto>;

            try
            {
                //*** check for document is valid or not
                if (mainMenuPatch == null)
                {
                    response.IsError = true;
                    response.ErrorMessage = "Invalid request";
                    return new ObjectResult(response) { StatusCode = (Int32)HttpStatusCode.BadRequest };
                }

                var entity = await _mainMenuRepository.GetAsync(id);
                if (entity == null)
                {
                    response.IsError = true;
                    response.ErrorMessage = "Request record was not found";
                    return new ObjectResult(response) { StatusCode = (Int32)HttpStatusCode.NotFound };
                }
                //*** map db entity to practice Dto
                var mainMenuDto = AutoMapper.Mapper.Map<MainMenuDto>(entity);
                //*** Apply patch
                mainMenuPatch.ApplyTo(mainMenuDto);

                AutoMapper.Mapper.Map(mainMenuDto, entity);
                var res = await _mainMenuRepository.UpdateAsync(entity);

                response.Model = mainMenuDto;
                response.Message = "The record was updated successfully";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse(RestMethod.PUT);
        }

        /// <summary>
        /// Delete an existing Cuisine
        /// </summary>
        /// <param name="id">MainMenu ID</param>
        /// <returns>Single response</returns>
        [HttpDelete]
        [Route("Cuisine/{id}", Name = "DeleteCuisine")]
        public async Task<IActionResult> DeleteCuisine(int id)
        {
            var response = new SingleModelResponse<MainMenuDto>() as ISingleModelResponse<MainMenuDto>;
            try
            {
                var entity = await _mainMenuRepository
                        .DeleteAsync(new MainMenu { Id = id });

                response.Model = entity.ToViewModel();
                response.Message = "The record was deleted successfully";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.ToString();
            }
            return response.ToHttpResponse();
        }
    }
}