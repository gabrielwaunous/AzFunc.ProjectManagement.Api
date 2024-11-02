using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetAllTasksByProjectAsync(int projectId);
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<TaskModel> CreateTaskAsync(TaskModel task);
    Task<TaskModel> UpdateTaskAsync(TaskModel task);
    Task<bool> DeleteTaskAsync(int id);
}