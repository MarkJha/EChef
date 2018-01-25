using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eCafe.WebUI.Filters;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Services.PropertyMapping;

namespace eCafe.WebUI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Base")]
    [ValidateModel]
    [ApiExceptionFilter]
    public class BaseController : Controller
    {
        public ITypeHelperService typeHelperService;
        public ILogger<BaseController> logger;
        public IUrlHelper urlHelper;
        public ILoggerRepository _loggingRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMappingService"></param>
        /// <param name="typeHelperService"></param>
        /// <param name="logger"></param>
        /// <param name="urlHelper"></param>
        public BaseController(
            ITypeHelperService typeHelperService,
            ILogger<BaseController> logger,
            IUrlHelper urlHelper,
            ILoggerRepository loggingRepository)
        {
            this.typeHelperService = typeHelperService;
            this.urlHelper = urlHelper;
            this.logger = logger;
            this._loggingRepository = loggingRepository;
            this.logger.LogInformation($"{this.GetType().Name} created");
        }
    }
}