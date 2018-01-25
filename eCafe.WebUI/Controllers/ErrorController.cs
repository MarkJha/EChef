using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Core.Entities;
using eCafe.WebUI.Filters;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace eCafe.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Error")]
    [ServiceFilter(typeof(LogFilter))]
    public class ErrorController : Controller
    {
        // ILoggerService _loggerService;
        private ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }


        //public ErrorController(ILoggerService loggerService)
        //{
        //    _loggerService = loggerService;
        //}

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Homepage was requested");
            try
            {
                throw new Exception("Oops! An Exception is going on!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.ToString());
            }
            return Ok("123");
        }

        [HttpPost]
        public JsonResult Error()
        {
            try
            {
                throw new InvalidOperationException("This is an unhandled exception");
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    DateCreated = DateTime.Now,
                    StackTrace = ex.StackTrace,
                    StatusCode = 500,
                    Logger = "ErrorController",
                    ActionName = "Error",
                    Exception = ex.Source,
                    Url = "api\\ErrorController\\Error"
                };
                //_loggerService.LogError(error);
            }
            return new JsonResult(Response.WriteAsync("An unexpected error occurred. please try again later."));
        }
    }
}