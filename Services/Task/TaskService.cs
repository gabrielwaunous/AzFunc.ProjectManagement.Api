using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalProjects.Function.Tasks;

public class TaskService(ITasksRepository repo) : ITaskService
{
    private readonly ITasksRepository _taskRepository = repo;

    public async Task<IEnumerable<TaskModel>> GetAllTasksByProjectAsync(int projectId)
    {
        return await _taskRepository.GetAllTasksByProjectAsync(projectId);
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