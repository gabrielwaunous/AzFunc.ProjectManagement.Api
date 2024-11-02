using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using PersonalProjects.Function.Tasks;

public class TasksRepository(IDbConnection dbConnection) : ITasksRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;
    public async Task<IEnumerable<TaskModel>> GetAllTasksByProjectAsync(int projectId)
    {
        try
        {
            var query = "SELECT * FROM Tasks WHERE project_id = @projectId";
            var tasks = await _dbConnection.QueryAsync<TaskModel>(query, new { projectId = projectId });
            return tasks;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving Tasks.", ex);
        }
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        try
        {
            var query = "SELECT * FROM tasks WHERE id = @id";
            var task = await _dbConnection.QueryFirstOrDefaultAsync<TaskModel>(query, new { id = id });
            return task;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving Task.", ex);
        }
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