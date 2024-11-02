using System;
using System.Collections.Generic;
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
    public class TasksFunction
    {
        private readonly ILogger<TasksFunction> _logger;
        private readonly ITaskService _taskService;

        public TasksFunction(ILogger<TasksFunction> log, ITaskService taskService)
        {
            _logger = log;
            _taskService = taskService;
        }

        [FunctionName("GetAllTasksByProject")]
        [OpenApiOperation(operationId: "GetAllTasksByProject", tags: new[] { "Tasks" })]
        [OpenApiParameter(name: "projectId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID of the Project")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(IEnumerable<TaskModel>), Description = "The list of task for the specified project")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No tasks found for the specified project")]
        public async Task<IActionResult> GetAllTasksByProject(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects/{projectId}/tasks")] HttpRequest req,
            int projectId
            )
            
        {
            var tasks = await _taskService.GetAllTasksByProjectAsync(projectId);

            if(tasks == null || !tasks.Any()){
                _logger.LogInformation($"No tasks found for project ID {projectId}.");
                return new NotFoundResult();
            }

            _logger.LogInformation($"Retrieved {tasks.Count()} projects for user ID {projectId}.");
            return new OkObjectResult(tasks);

        }

        [FunctionName("GetTaskById")]
        [OpenApiOperation(operationId: "GetTaskById", tags: new[] { "Tasks"})]
        [OpenApiParameter(name: "taskId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID of the Task")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(TaskModel), Description = "the Task of specified Id")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No tasks found for the specified id")]
        public async Task<IActionResult> GetTaskById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks/{taskId}")] HttpRequest req,
            int taskId
        )
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            
            if(task == null)
            {
                _logger.LogInformation($"No task was found!");
                return new NotFoundResult();
            }

            return new OkObjectResult(task);
        }
    }
}

