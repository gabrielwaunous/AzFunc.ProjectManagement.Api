using System;
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

namespace PersonalProjects.Function
{
    public class ProjectFunction
    {
        private readonly ILogger<ProjectFunction> _logger;

        public ProjectFunction(ILogger<ProjectFunction> log)
        {
            _logger = log;
        }

        [FunctionName("GetAllProjectsByUser")]
        [OpenApiOperation(operationId: "GetAllProjectsByUser", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(long), Description = "ID of the User")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<Project>), Description = "The list of projects for the specified user")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No projects found for the specified user")]
        public async Task<IActionResult> GetAllProjectsByUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{userId}/projects")] HttpRequest req,
            int userId
        )
        {
            throw new NotImplementedException();
        }
    }
}

