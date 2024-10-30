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
using System.Linq;
using Microsoft.CodeAnalysis;


namespace PersonalProjects.Function
{
    public class ProjectFunction
    {
        private readonly ILogger<ProjectFunction> _logger;
        private readonly IProjectService _projectService;
        public ProjectFunction(ILogger<ProjectFunction> log, IProjectService projectService)
        {
            _logger = log;
            _projectService = projectService;
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
            var projects = await _projectService.GetAllProjectsByUserAsync(userId);

            if (projects == null || !projects.Any())
            {
                _logger.LogInformation($"No projects found for user ID {userId}.");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Retrieved {projects.Count()} projects for user ID {userId}.");
            return new OkObjectResult(projects);
        }

        [FunctionName("GetProjectById")]
        [OpenApiOperation(operationId: "GetProjectById", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "projectId", In = ParameterLocation.Path, Required = true, Type = typeof(long), Description = "ID of the Project")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Project), Description = "The projects for the specified id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No projects found for the specified id")]
        public async Task<IActionResult> GetProjectById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "project/{projectId}")] HttpRequest req,
            int projectId
        )
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                _logger.LogInformation($"No project was found!");
                return new NotFoundResult();
            }

            return new OkObjectResult(project);

        }

        [FunctionName("CreateProject")]
        [OpenApiOperation(operationId: "CreateProject", tags: new[] { "Projects" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Project), Description = "Project to create", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Project), Description = "The project user")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No project was created")]
        public async Task<IActionResult> CreateProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Projects")] HttpRequest req
        )
        {
            _logger.LogInformation($"Creating new Project...");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var project = JsonConvert.DeserializeObject<Project>(requestBody);

            if (project == null)
            {
                return new BadRequestResult();
            }

            await _projectService.CreateProjectAsync(project);
            _logger.LogInformation("Project created successfully with ID: {ProjectId}", project.id);

            var sanitizedProject = project.ToString().Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ");
            _logger.LogInformation($"Created a project: {sanitizedProject}.");

            return new CreatedResult($"/entities/{project.id}", project);
        }

        [FunctionName("UpdateProject")]
        [OpenApiOperation(operationId:"UpdateProject", tags: new[] { "Projects" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Project), Description = "Project to update", Required = true)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The project was updated")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Project not found")]
        public async Task<IActionResult> UpdateProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "project")] HttpRequest req
        )
        {            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var project = JsonConvert.DeserializeObject<Project>(requestBody);
            if (project == null)
            {
                return new BadRequestResult();
            }
            
            _logger.LogInformation($"Updating project with id = {project.id}.");

            var existingProject = await _projectService.GetProjectByIdAsync(project.id);
            if(existingProject == null)
            {
                return new NotFoundResult();
            }

            await _projectService.UpdateProjectAsync(project);
            return new NoContentResult();

        }

        [FunctionName("DeleteProject")]
        [OpenApiOperation(operationId: "DeleteProject", tags: new[] { "Projects" })]
        [OpenApiParameter(name: "projectId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID of the project to delete")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The project was deleted")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Project not found")]
        public async Task<IActionResult> DeleteProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "project/{projectId}")] HttpRequest req,
            int projectId
        )
        {
            _logger.LogInformation($"Deleting entity with id = {projectId}.");

            var existingProject = await _projectService.GetProjectByIdAsync(projectId);
            if (existingProject == null)
            {
                return new NotFoundResult();
            }

            await _projectService.DeleteProjectAsync(projectId);
            return new NoContentResult();
        }
    }
}

