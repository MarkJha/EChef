using eCafe.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace eCafe.WebUI.Responses
{
    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse<TModel>(this IListModelResponse<TModel> response, RestMethod methodType = RestMethod.GET)
        {
            var status = HttpStatusCode.OK;

            if (response.IsError)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Model == null && response.ShapeModel == null)
            {
                status = HttpStatusCode.NotFound;
            }
            else if (methodType.Equals(RestMethod.PUT) || methodType.Equals(RestMethod.DELETE))
            {
                status = HttpStatusCode.NoContent;
            }

            return new ObjectResult(response) { StatusCode = (Int32)status };
        }

        public static IActionResult ToHttpResponse<TModel>(this ISingleModelResponse<TModel> response, RestMethod methodType = RestMethod.GET)
        {
            var status = HttpStatusCode.OK;

            if (response.IsError)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Model == null)
            {
                status = HttpStatusCode.NotFound;
            }
            else if (methodType.Equals(RestMethod.PUT) || methodType.Equals(RestMethod.DELETE))
            {
                status = HttpStatusCode.NoContent;
            }

            return new ObjectResult(response) { StatusCode = (Int32)status };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="response"></param>
        /// <param name="errorMessage"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IActionResult ToErrorResponse<TModel>(this IListModelResponse<TModel> response, string errorMessage, HttpStatusCode status)
        {
            response.PageSize = 0;
            response.PageNumber = 0;
            response.IsError = true;
            response.ErrorMessage = errorMessage;
            return new ObjectResult(response) { StatusCode = (Int32)status };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="response"></param>
        /// <param name="errorMessage"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IActionResult ToErrorResponse<TModel>(this ISingleModelResponse<TModel> response, string errorMessage, HttpStatusCode status)
        {
            response.IsError = true;
            response.ErrorMessage = errorMessage;
            return new ObjectResult(response) { StatusCode = (Int32)status };
        }


    }
}
