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

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetTaskByIdAsync(id);
    }
    public async Task<int> CreateTaskAsync(TaskModel task)
    {
        return await _taskRepository.CreateTaskAsync(task);
    }

    public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
    {
        return await _taskRepository.UpdateTaskAsync(task);
    }
    public async Task<bool> DeleteTaskAsync(int id)
    {
        return await _taskRepository.DeleteTaskAsync(id);
    }


}