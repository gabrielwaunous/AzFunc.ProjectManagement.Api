using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalProjects.Function.Tasks
{
    public interface ITasksRepository
    {
        public Task<IEnumerable<TaskModel>> GetAllTasksByProjectAsync(int userId);
        public Task<TaskModel> GetTaskByIdAsync(int id);
        public Task<int> CreateTaskAsync(TaskModel task);
        public Task<TaskModel> UpdateTaskAsync(TaskModel task);
        public Task<bool> DeleteTaskAsync(int id);
    }
}
