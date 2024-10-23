using System.Collections.Generic;
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
using PersonalProjects.Function.Services;

namespace PersonalProjects.Function
{
    public class UserFunction
    {
        private readonly ILogger<UserFunction> _logger;
        private readonly IUserService _userService;

        public UserFunction(ILogger<UserFunction> log, IUserService user)
        {
            _logger = log;
            _userService = user;
        }

        [FunctionName("GetEntities")]
        [OpenApiOperation(operationId: "GetEntities", tags: new[] { "Entities" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<User>), Description = "List of entities")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No entities found")]
        public async Task<IActionResult> GetEntities(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get" , Route = "entities")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

                    var entities = await _userService.GetAllEntitiesAsync();
        if (entities == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(entities);
        }
    }
}

