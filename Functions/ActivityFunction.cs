using System;
using System.IO;
using System.Linq;
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

        [FunctionName("GetAllActivitiesByProject")]
        [OpenApiOperation(operationId: "GetAllActivitiesByProject", tags: new[]{"Activities"})]
        [OpenApiParameter(name: "projectId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The Project id of activities")]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Activity), Description = "The Activity list for specified project Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No activities found for specified project id")]
        public async Task<IActionResult> GetAllActivitiesByProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "project/{projectId}/activities")] HttpRequest req,
            int projectId
        )
        {
            var activityList = await _activityService.GetAllActivitiesByProjectAsync(projectId);
            
            if(!activityList.Any()) return new NotFoundResult();

            return new OkObjectResult(activityList);
        }
        
        [FunctionName("GetAllActivitiesByUser")]
        [OpenApiOperation(operationId: "GetAllActivitiesByUser", tags: new[]{"Activities"})]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The User id of activities")]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Activity), Description = "The Activity list for specified user Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No activities found for specified user id")]
        public async Task<IActionResult> GetAllActivitiesByUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{userId}/activities")] HttpRequest req,
            int userId
        )
        {
            var activityList = await _activityService.GetAllActivitiesByProjectAsync(userId);
            
            if(!activityList.Any()) return new NotFoundResult();

            return new OkObjectResult(activityList);
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

