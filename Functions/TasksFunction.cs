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

        [FunctionName("CreateTask")]
        [OpenApiOperation(operationId: "CreateTask", tags: new[] { "Tasks"})]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(TaskModel), Description = "The Task to create", Required = true )]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(TaskModel), Description = "The created Task")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No Task was created")]
        public async Task<IActionResult> CreateTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Tasks")] HttpRequest req
        )
        {
            _logger.LogInformation("Creating new task...");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var task = JsonConvert.DeserializeObject<TaskModel>(requestBody);

            if(task == null)
            {
                return new BadRequestResult();
            }

            var taskId = await _taskService.CreateTaskAsync(task);
            
            _logger.LogInformation("Project created successfully with ID: {ProjectId}", taskId);

            var sanitizedProject = task.ToString().Replace(Environment.NewLine, " ").Replace("\n", " ").Replace("\r", " ");
            _logger.LogInformation($"Created a task: {sanitizedProject}.");

            return new CreatedResult($"/task/{taskId}", task);
        }

        [FunctionName("UpdateTask")]
        [OpenApiOperation(operationId: "UpdateTask", tags: new[] {"Tasks"})]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(TaskModel), Description = "The Task to update", Required = true)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The task was updated")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Task was not found")]
        public async Task<IActionResult> UpdateTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Tasks")] HttpRequest req
        )
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var task = JsonConvert.DeserializeObject<TaskModel>(requestBody);

            if(task == null ) return new BadRequestResult();

            var existingProject = await _taskService.GetTaskByIdAsync(task.id);
            
            if(existingProject == null ) return new NotFoundResult();

            await _taskService.UpdateTaskAsync(task);

            return new NoContentResult();
        }

        [FunctionName("DeleteTask")]
        [OpenApiOperation(operationId:"DeleteTask", tags: new[] {"Tasks"})]
        [OpenApiParameter(name: "taskId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID of the Task")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The Task was deleted")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Task not found")]
        public async Task<IActionResult> DeleteTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Tasks/{taskId}")] HttpRequest req,
            int taskId
        )
        {
            _logger.LogInformation($"Deleting Task with id = {taskId}.");

            var existingTask = await _taskService.GetTaskByIdAsync(taskId);
            
            if(existingTask == null) return new NotFoundResult();

            await _taskService.DeleteTaskAsync(existingTask.id);
            return new NoContentResult();
        }
    }
}

