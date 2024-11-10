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

        [FunctionName("GetActivityById")]
        [OpenApiOperation(operationId: "GetActivityById", tags: new[] { "Activities" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Activity Id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Activity), Description = "The Activity Requested")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No Activity was not found for the specified id")]
        public async Task<IActionResult> GetActivityById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "activity/{id}")] HttpRequest req,
            int id
            )
        {
            _logger.LogInformation($"Searching Activity with id: {id}");
            
            var activity = await _activityService.GetActivityByIdAsync(id);

            if(activity == null ) return new NotFoundResult();

            return new OkObjectResult(activity);
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

