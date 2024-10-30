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

        public UserFunction(ILogger<UserFunction> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [FunctionName("GetUsers")]
        [OpenApiOperation(operationId: "GetUsers", tags: new[] { "Users" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<User>), Description = "List of Users")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No user found")]
        public async Task<IActionResult> GetEntities(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var entities = await _userService.GetAllEntitiesAsync();
            if (entities == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(entities);
        }

        [FunctionName("GetUserById")]
        [OpenApiOperation(operationId: "GetUserById", tags: new[] { "Users" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID of the User")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Description = "The user with the specified ID")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "User not found")]
        public async Task<IActionResult> GetUserById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequest req,
            int id
        )
        {
            _logger.LogInformation($"Fetching entity with id = {id}.");

            var user = await _userService.GetEntityByIdAsync(id);
            if(user == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(user);
        }

        [FunctionName("CreateUser")]
        [OpenApiOperation(operationId: "CreateUser", tags: new[] { "Users" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Description = "User to create", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(User), Description = "The created user")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Users")] HttpRequest req
        )
        {
            _logger.LogInformation("Creating a new entity.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);

            if (user == null)
            {
                return new BadRequestResult();
            }

            _logger.LogInformation("Received user: {@User}", user);
            await _userService.CreateEntityAsync(user);
            _logger.LogInformation("User created successfully with ID: {UserId}", user.id);

            _logger.LogInformation($"Created a user: {user}.");
            return new CreatedResult($"/entities/{user.id}", user);
        }

        [FunctionName("UpdateUser")]
        [OpenApiOperation(operationId: "UpdateUser", tags: new[] { "Users" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Description = "User to update", Required = true)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The entity was updated")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "User not found")]
        public async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id}")] HttpRequest req,
            int id
        )
        {
            _logger.LogInformation($"Updating user with id = {id}.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);
            if (user == null)
            {
                return new BadRequestResult();
            }

            var existingEntity = await _userService.GetEntityByIdAsync(id);
            if (existingEntity == null)
            {
                return new NotFoundResult();
            }

            user.id = id;
            await _userService.UpdateEntityAsync(user);
            return new NoContentResult();
        }

        [FunctionName("DeleteUser")]
        [OpenApiOperation(operationId: "DeleteUser", tags: new[] { "Users" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID of the user to delete")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The user was deleted")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "User not found")]
        public async Task<IActionResult> DeleteUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{id}")] HttpRequest req,
            int id
        )
        {
            _logger.LogInformation($"Deleting entity with id = {id}.");

            var existingEntity = await _userService.GetEntityByIdAsync(id);
            if (existingEntity == null)
            {
                return new NotFoundResult();
            }

            await _userService.DeleteEntityAsync(id);
            return new NoContentResult();
        }
    }
}

