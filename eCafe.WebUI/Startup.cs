using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eCafe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AspNetCoreRateLimit;
using NLog.Web;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using eCafe.Infrastructure;
using eCafe.WebUI.Services.ApiGetAll;
using eCafe.Core.Entities;
using eCafe.WebUI.Filters;
using FluentValidation.AspNetCore;
using eCafe.Infrastructure.Repository;
using eCafe.Infrastructure.Services;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Services.PropertyMapping;
using System.Reflection;
using NLog;

namespace eCafe_WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure connection string for application
            ConfigureConnectionString(services);
            // Configure DI
            ConfigureDI(services);
            // Configure rate limit options
            // ConfigureRateLimit(services);
            // Configure output formatters services
            ConfigureOutputFormatters(services);
            // Configure Cache
            //ConfigureCache(services);
            // Add HttpCacheHeaders services with custom options eg. If-None-Match --ETag
            // ConfigureHttpCacheHeaders(services);
            // Register the Swagger generator, defining one or more Swagger documents
            ConfigureSwagger(services);
            // Configure API Version for all methods
            ConfigureApiVersioning(services);
            // Configure CORS for API
            ConfigureCors(services);
            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<LogFilter>();
            //services.AddMvc()
            //.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Configure Logging
            ConfigureLog(app, loggerFactory, env);

            // Initialize auto-mapper
            InitializeAutoMapper();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //app.UseAuthentication();

            //app.UseIpRateLimiting();

            // add Response Caching middleware to the request pipeline
            //app.UseResponseCaching();

            // adds HttpCache headers to responses (Cache-Control, Expires, ETag, Last-Modified), and implements cache expiration & validation models
            //app.UseHttpCacheHeaders();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private void ConfigureConnectionString(IServiceCollection services)
        {
            services.AddDbContext<ECafeContext>(c =>
            {
                try
                {
                    //c.UseInMemoryDatabase("Catalog");
                    c.UseSqlServer(Configuration.GetConnectionString("eCafeConnection"));
                    c.ConfigureWarnings(wb =>
                    {
                        //By default, in this application, we don't want to have client evaluations
                        wb.Log(RelationalEventId.QueryClientEvaluationWarning);
                    });
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            });
        }

        private void ConfigureApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("ver", "X-eCafeApi-Version");
            });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("All", bldr =>
                {
                    bldr.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                       // .WithOrigins("http://localhost:4200");
                });

                cfg.AddPolicy("AnyGET", bldr =>
                {
                    bldr.AllowAnyHeader()
                        .WithMethods("GET")
                        .AllowAnyOrigin();
                });
            });
        }

        private void ConfigureCache(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 60
                    });
                options.CacheProfiles.Add("Never",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
                options.CacheProfiles.Add("MyCache",
                    new CacheProfile()
                    {
                        Duration = 60,
                        Location = ResponseCacheLocation.Client
                    });
            });

            //services.AddResponseCaching();
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;
            });
        }

        private static void ConfigureHttpCacheHeaders(IServiceCollection services)
        {
            services.AddHttpCacheHeaders(
                (expirationModelOptions) =>
                {
                    expirationModelOptions.MaxAge = 600;
                    expirationModelOptions.SharedMaxAge = 300;
                },
                (validationModelOptions) =>
                {
                    validationModelOptions.AddMustRevalidate = true;
                    validationModelOptions.AddProxyRevalidate = true;
                });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "eClinical Works API",
                    Description = "Complete health care service API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Markandeshwar Jha", Email = "mark_jha@hotmail.com", Url = "https://twitter.com/spboyer" },
                    License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });

                //Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "eCafe.WebUI.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureOutputFormatters(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                // Add XML Data contract serializer
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                var xmlDataContractSerializerInputFormatter =
                new XmlDataContractSerializerInputFormatter();
                xmlDataContractSerializerInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.marvin.authorwithdateofdeath.full+xml");
                setupAction.InputFormatters.Add(xmlDataContractSerializerInputFormatter);

                var jsonInputFormatter = setupAction.InputFormatters
                .OfType<JsonInputFormatter>().FirstOrDefault();

                if (jsonInputFormatter != null)
                {
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.marvin.author.full+json");
                    jsonInputFormatter.SupportedMediaTypes
                    .Add("application/vnd.marvin.authorwithdateofdeath.full+json");
                }

                var jsonOutputFormatter = setupAction.OutputFormatters
                    .OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.hateoas+json");
                }

            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            });
        }

        private void ConfigureDI(IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IMainMenuRepository, MainMenuRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuDetailRepository, MenuDetailRepository>();
            services.AddScoped<IMenuImageRepository, MenuImageRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper, UrlHelper>((urlHrlperFactory =>
            {
                var actionContext = urlHrlperFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            }));

            // Services     
            services.AddTransient<ITypeHelperService, TypeHelperService>();

            services.AddTransient<IMainMenuService, MainMenuService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IMenuDetailService, MenuDetailService>();
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<IMainMenuGet, MainMenuGet>();
            services.AddTransient<IMenuGet, MenuGet>();
            services.AddTransient<IPropertyMappingService, MainMenuPropertyMappingService>();
            services.AddTransient<IPropertyMappingService, MenuPropertyMappingService>();

            //needed for NLog.Web
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
             

        private void ConfigureRateLimit(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>((options) =>
            {
                options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
                {
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 20,
                        Period = "15s"
                    },
                    new RateLimitRule()
                    {
                        Endpoint = "*",
                        Limit = 3,
                        Period = "10s"
                    }
                };
            });

            // add Rate limit service
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        }

        private void InitializeAutoMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Doctor, Api.Models.DoctorDto>()
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.MiddleName} {src.LastName}"))
                //.ForMember(dest => dest.Age, opt => opt.MapFrom(src => AgeExtension.ToAge(src.DOB ?? new DateTime())));

                cfg.CreateMap<MainMenu, MainMenuDto>();
                cfg.CreateMap<MainMenuDto, MainMenu>();

                //cfg.CreateMap<MedicineType, Api.Models.MedicineTypeDto>();
                //cfg.CreateMap<Api.Models.MedicineTypeDto, MedicineType>();
            });
        }

        private void ConfigureLog(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            // add NLog to .NET Core
            loggerFactory.AddNLog();

            //Enable ASP.NET Core features (NLog.web) - only needed for ASP.NET Core users
            app.AddNLogWeb();

            //needed for non-NETSTANDARD platforms: configure nlog.config in your project root. NB: you need NLog.Web.AspNetCore package for this. 
            
            env.ConfigureNLog("nlog.config");
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("eCafeConnection");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            //else
            //{
            //    // handling exception across application level
            //    app.UseExceptionHandler(appBuilder =>
            //    {
            //        appBuilder.Run(async context =>
            //        {
            //            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
            //            if (exceptionHandler != null)
            //            {
            //                var logger = loggerFactory.CreateLogger("Global exception Logger");
            //                logger.LogError(500, exceptionHandler.Error, exceptionHandler.Error.Message);
            //            }

            //            context.Response.StatusCode = 500;
            //            await context.Response.WriteAsync("An unexpected error occurred. please try again later.");
            //        });
            //    });
            // app.UseExceptionHandler("/Home/Error");
            //}
        }
    }
}
