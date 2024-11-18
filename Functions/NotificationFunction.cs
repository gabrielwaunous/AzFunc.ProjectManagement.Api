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
    public class NotificationFunction
    {
        private readonly ILogger<NotificationFunction> _logger;
        private readonly INotificationService _service;

        public NotificationFunction(ILogger<NotificationFunction> log, INotificationService service)
        {
            _logger = log;
            _service = service;
        }

        [FunctionName("GetNotificationById")]
        [OpenApiOperation(operationId: "GetNotificationById", tags: new[] { "Notifications" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The notification id")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Notification), Description = "The Notification object")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No Notification was found with given id")]
        public async Task<IActionResult> GetNotificationById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "notification/{id}")] HttpRequest req,
            int id
        )
        {
            _logger.LogInformation($"Searching Notification with id: {id}");

            var notification = await _service.GetNotificacionById(id);

            if(notification == null ) return new NotFoundResult();

            return new OkObjectResult(notification);
        }
    }
}

