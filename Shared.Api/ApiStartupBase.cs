using DevExtreme.AspNet.Data;
using eveDirect.Caching;
using eveDirect.Shared.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.Api
{
    /// <summary>
    /// Базвый Startup класс для asp.net core api
    /// </summary>
    public class ApiStartupBase
    {
        protected IConfiguration configuration { get; }
        protected string _nameApi { get; set; }
        public ApiStartupBase(IConfiguration configuration, string nameApi)
        {
            this.configuration = configuration;
            _nameApi = nameApi;
        }
        protected void ApiConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMVC(configuration)
                .AddCustomOptions(configuration)
                .AddSwagger(configuration, _nameApi)
                .AddStackExchangeRedisCache(action =>
                {
                    var conStr = configuration["Redis:Address"];
                    if (conStr == null)
                    {
                        Log.Warning("Старт БЕЗ кэширования Redis");
                    }
                    else
                    {
                        action.Configuration = conStr;
                        action.InstanceName = $"{_nameApi}";
                        //if (Debugger.IsAttached)
                        //{
                            action.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                            {
                                Password = configuration["Redis:Password"],
                                AsyncTimeout = 20 * 1000,
                                SyncTimeout = 20 * 1000,
                                EndPoints =
                            {
                                { conStr, 6379 }
                            },
                            };
                        //}
                    }
                })
                .AddSingleton<ICustomDistibutedCache, CustomDistibutedCache>()
                ;

            //var container = new ContainerBuilder();
            //container.Populate(services);

            //return new AutofacServiceProvider(container.Build());
        }
        protected void ApiConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Debugger.IsAttached)
                app.UseDeveloperExceptionPage();
            else
                app.UseHttpsRedirection();

            var pathBase = configuration["PATH_BASE"];

            //if (Debugger.IsAttached)
            //{
                app
                .UseSwagger()
                .UseSwaggerUI(setupAction =>
                {
                    setupAction.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/1/swagger.json", $"{_nameApi} V1");
                });
            //}

            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string nameApi)
        {
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("1", new OpenApiInfo
                {
                    Title = $"eveDirect - {nameApi} HTTP API",
                    Version = "1"
                });

                options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));

                //options.EnableAnnotations();
                options.OperationFilter<TagByApiExplorerSettingsOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));

                    // Добавляем ко всем нашим action() типы ответов
                    options.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                    options.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                    options.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

                    // Запрет доступа по http
                    if (!Debugger.IsAttached)
                        options.ReturnHttpNotAcceptable = true;
                }
                )
                .AddJsonOptions(conf =>
                {
                    conf.JsonSerializerOptions.PropertyNamingPolicy = null;
                    conf.JsonSerializerOptions.Converters.Add(new JsonCustomConverter());
                })
                ;
            //.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<CatalogSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var problemDetails = new ValidationProblemDetails(actionContext.ModelState)
                    {
                        Instance = actionContext.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };

                    //var actionExecutionContext = actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    //if (actionContext.ModelState.ErrorCount > 0 && actionExecutionContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                        //return new UnprocessableEntityObjectResult(actionContext.ModelState);

                    //if one of the keys wasn't correctly found / counn't be parsed
                    // we're dealing with null/unparsable input
                    //return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            return services;
        }
    }

    public class TagByApiExplorerSettingsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var apiExplorerSettings = controllerActionDescriptor
                    .ControllerTypeInfo.GetCustomAttributes(typeof(ApiExplorerSettingsAttribute), true)
                    .Cast<ApiExplorerSettingsAttribute>().FirstOrDefault();
                if (apiExplorerSettings != null && !string.IsNullOrWhiteSpace(apiExplorerSettings.GroupName))
                {
                    operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = apiExplorerSettings.GroupName } };
                }
                else
                {
                    operation.Tags = new List<OpenApiTag>
                    {new OpenApiTag {Name = controllerActionDescriptor.ControllerName}};
                }
            }
        }
    }

    public class JsonCustomConverter : JsonConverter<DevExtreme.AspNet.Data.DataSourceLoadOptionsBase>
    {
        public override DataSourceLoadOptionsBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var dbOptions = new DataSourceLoadOptionsBase();
            string propertyName = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject && reader.CurrentDepth == 1)
                {
                    // Выход из парсинга
                    return dbOptions;
                }

                if (reader.TokenType == JsonTokenType.PropertyName && reader.CurrentDepth == 2)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                }

                switch (propertyName)
                {
                    case "Take":
                        dbOptions.Take = reader.GetInt32();
                        break;

                    case "Skip":
                        dbOptions.Skip = reader.GetInt32();
                        break;

                    case "RequireTotalCount":
                        dbOptions.RequireTotalCount = reader.GetBoolean();
                        break;

                    case "RequireGroupCount":
                        dbOptions.RequireGroupCount = reader.GetBoolean();
                        break;

                    case "IsCountQuery":
                        dbOptions.IsCountQuery = reader.GetBoolean();
                        break;

                    case "Filter":
                        var val = ExtractValue(ref reader, options);
                        if(val is List<object>)
                            dbOptions.Filter = (List<object>)val;

                        break;

                    case "Sort":
                        //var val3 = ExtractValue(ref reader, options);
                        //if (val3 is List<object>)
                        //    dbOptions.Sort = ((List<object>)val3).Select(x => x as SortingInfo).ToArray();
                        dbOptions.Sort = JsonSerializer.Deserialize<SortingInfo[]>(ref reader, options);

                        break;

                    case "Group":
                        var val2 = ExtractValue(ref reader, options);
                        if (val2 is List<object>)
                            dbOptions.Group = ((List<object>)val2).Select(x => x as GroupingInfo).ToArray();

                        break;

                    case "TotalSummary":
                        var val1 = ExtractValue(ref reader, options);
                        if (val1 is List<object>)
                            dbOptions.TotalSummary = ((List<object>)val1).Select(x => x as SummaryInfo).ToArray();

                        break;
                    case "GroupSummary":
                        var val4 = ExtractValue(ref reader, options);
                        if (val4 is List<object>)
                            dbOptions.GroupSummary = ((List<object>)val4).Select(x => x as SummaryInfo).ToArray();

                        break;

                    case "Select":
                        var val5 = ExtractValue(ref reader, options);
                        if (val5 is List<object>)
                            dbOptions.Select = ((List<string>)val5).Select(x => x as string).ToArray();

                        break;
                }

            }

            throw new JsonException();
        }

        private object ExtractValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    if (reader.TryGetDateTime(out var date))
                    {
                        return date;
                    }
                    return reader.GetString();

                case JsonTokenType.False:
                    return false;

                case JsonTokenType.True:
                    return true;

                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out var result))
                    {
                        return result;
                    }
                    return reader.GetDecimal();

                case JsonTokenType.StartObject:
                    return ReadObject(ref reader, null, options);

                case JsonTokenType.StartArray:
                    var list = new List<object>();
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        list.Add(ExtractValue(ref reader, options));
                    }
                    return list;

                default:
                    throw new JsonException($"'{reader.TokenType}' is not supported");
            }
        }

        private object ReadObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string propertyName = "";
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                }

                switch (reader.TokenType)
                {
                    case JsonTokenType.String:
                        if (reader.TryGetDateTime(out var date))
                        {
                            return date;
                        }
                        return reader.GetString();

                    case JsonTokenType.False:
                        return false;

                    case JsonTokenType.True:
                        return true;

                    case JsonTokenType.Null:
                        return null;

                    case JsonTokenType.Number:
                        if (reader.TryGetInt64(out var result))
                        {
                            return result;
                        }
                        return reader.GetDecimal();

                    case JsonTokenType.StartObject:
                        return ReadObject(ref reader, null, options);

                    case JsonTokenType.StartArray:
                        var list = new List<object>();
                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            list.Add(ExtractValue(ref reader, options));
                        }
                        return list;

                    default:
                        throw new JsonException($"'{reader.TokenType}' is not supported");
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DataSourceLoadOptionsBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
