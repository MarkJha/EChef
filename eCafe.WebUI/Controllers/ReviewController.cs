using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Services.PropertyMapping;
using Microsoft.Extensions.Logging;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Models;
using eCafe.WebUI.Responses;
using eCafe.Infrastructure.Constants;
using System.Net;

namespace eCafe.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Review")]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService, ITypeHelperService typeHelperService,
           ILogger<ReviewController> logger,
           IUrlHelper urlHelper,
           ILoggerRepository loggerRepository) :
            base(typeHelperService, logger, urlHelper, loggerRepository)
        {
            _reviewService = reviewService;
            base.logger.LogInformation($"{this.GetType().Name} created");
        }

        /// <summary>
        /// Get All Given Reviews from Customers
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Reviews", Name = "GetAllReviewsDetails")]
        public async Task<IActionResult> GetAllReviewsDetails(Int32? pageSize = 10, Int32? pageNumber = 1, String name = null)
        {
            var response = new ListModelResponse<ReviewDto>() as IListModelResponse<ReviewDto>;
            response.PageSize = (Int32)pageSize;
            response.PageNumber = (Int32)pageNumber;
            response.Model = await _reviewService.GetReviewAsync(response.PageSize, response.PageNumber, name);
            response.Message = $"{ApiMessages.TotalRecords} {response.Model.Count()}";
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Add new review for our Menus
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create", Name = "CreateReview")]
        [Produces(ApiMessages.ApplicationJson, Type = typeof(ReviewDto))]
        public async Task<IActionResult> CreateReview([FromBody]ReviewDto model)
        {
            var response = CreateSingleModelResponse();
            //*** check for model is valid or not if invalid returning bad request response
            if (model == null)
            {
                return response.ToErrorResponse(ApiMessages.InvalidRequest, HttpStatusCode.BadRequest);
            }

            model.Guid = Guid.NewGuid();
            model.IsActive = false;
            var entity = await _reviewService.CreateReviewAsync(model);
            response.Model = entity;
            response.Message = entity.Id != 0 ? ApiMessages.SavedRecord : ApiMessages.SavedFailedRecord;
            return response.ToHttpResponse();
            //return CreatedAtRoute("GetAll", new { id = response.Model.Id }, response.Model);
        }

        /// <summary>
        /// delete the Review from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteReviewDetail")]
        public async Task<IActionResult> DeleteReviewDetail(int id)
        {
            var response = CreateSingleModelResponse();
            var entity = await _reviewService.DeleteReviewAsync(id);
            response.Model = entity;
            response.Message = entity.Id != 0 ? ApiMessages.DeletedRecord : ApiMessages.DeletedFailedRecord;
            return response.ToHttpResponse();
        }

        private static ISingleModelResponse<ReviewDto> CreateSingleModelResponse()
        {
            return new SingleModelResponse<ReviewDto>() as ISingleModelResponse<ReviewDto>;
        }
    }
}