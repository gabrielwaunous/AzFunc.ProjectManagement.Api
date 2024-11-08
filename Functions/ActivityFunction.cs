using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace PersonalProjects.Function
{
    public class ActivityFunction
    {
        private readonly ILogger<ActivityFunction> _logger;
        private readonly IActivityService _activityService;

        public ActivityFunction(ILogger<ActivityFunction> log, IActivityService service)
        {
            _logger = log;
            _activityService = service;
        }

        [FunctionName("ActivityFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            throw new NotImplementedException();
        }

        [FunctionName("CreateActivity")]
        [OpenApiOperation(operationId: "CreateActivity", tags: new[] { "Activities" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Activity), Description = "Activity to Create", Required = true )]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Activity), Description = "Created Activity")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No Activity was created")]
        public async Task<IActionResult> CreateActivity (
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Activity")] HttpRequest req            
        )
        {
            _logger.LogInformation($"Creating new Activity...");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var activity = JsonConvert.DeserializeObject<Activity>(requestBody);

            if (activity == null)
            {
                return new BadRequestResult();
            }

            await _activityService.CreateActivityAsync(activity);            

            var sanitizedActivity = activity.ToString().Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ");
            _logger.LogInformation($"Created a Activity: {sanitizedActivity}.");

            return new CreatedResult($"/activity/{activity.id}", sanitizedActivity);
        }
    }
}

