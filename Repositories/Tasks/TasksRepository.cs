using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
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
    public async Task<int> CreateTaskAsync(TaskModel task)
    {
        var insertQuery = @"
                INSERT INTO [dbo].[TASKS] (project_id, name, due_date, priority, status)
                VALUES (@project_id, @name, @due_date, @priority, @status);
                SELECT CAST(SCOPE_IDENTITY() AS int);
            ";
        var parameters = new { task.project_id, task.name, task.due_date, task.priority, task.status };
        var tasksId = await _dbConnection.QueryFirstOrDefaultAsync<int>(insertQuery, parameters);
        return tasksId;
    }

    public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
    {
        var updateQuery = @"
            UPDATE [dbo].[TASKS]
                SET project_id = @project_id, name = @name, due_date = @due_date, priority = @priority, status = @status
            WHERE id = @id
        ";

        var parameters = new {task.id, task.project_id ,task.name, task.due_date, task.priority, task.status };

        await _dbConnection.ExecuteAsync(updateQuery, parameters);

        return task;
    }
    public async Task<bool> DeleteTaskAsync(int id)
    {
        var deleteQuery = @"
            DELETE FROM [dbo].[TASKS] WHERE id = @id
        ";

        var result = await _dbConnection.ExecuteAsync(deleteQuery, new { id = id });
        
        return result > 0;
    }
}