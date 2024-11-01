using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalProjects.Function.Tasks;

public class TasksRepository : ITasksRepository
{
    public Task<IEnumerable<TaskModel>> GetAllTasksByProjectAsync(int userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<TaskModel> GetTaskByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }
    public Task<int> CreateTaskAsync(TaskModel task)
    {
        throw new System.NotImplementedException();
    }

    public Task<Project> UpdateTaskAsync(TaskModel task)
    {
        throw new System.NotImplementedException();
    }
    public Task<bool> DeleteTaskAsync(int id)
    {
        throw new System.NotImplementedException();
    }
}