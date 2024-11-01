using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalProjects.Function.Tasks;

public class TaskService(ITasksRepository repo) : ITaskService
{
    private readonly ITasksRepository _taskRepository = repo;

    public Task<IEnumerable<TaskModel>> GetAllTasksByProjectAsync(int projectId)
    {
        throw new System.NotImplementedException();
    }

    public Task<TaskModel> GetProjectByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }
    public Task<TaskModel> CreateTaskAsync(TaskModel task)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteTaskAsync(int id)
    {
        throw new System.NotImplementedException();
    }


    public Task<TaskModel> UpdateTaskAsync(TaskModel task)
    {
        throw new System.NotImplementedException();
    }
}