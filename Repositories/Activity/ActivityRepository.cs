using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

public class ActivityRepository : IActivityRepository
{
    private readonly IDbConnection _dbConnection;

    public ActivityRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Activity> GetActivityByIdAsync(int id)
    {
        var query = @"
            SELECT [id]
                ,[project_id]
                ,[user_id]
                ,[activity_type]
                ,[description]
            FROM [ProjectManagementDB].[dbo].[ACTIVITIES]
            WHERE [id] = @id
        ";

        var activity = await _dbConnection.QueryFirstOrDefaultAsync<Activity>(query, new { id });
        return activity;
    }

    public Task<IEnumerable<Activity>> GetActivityByProjectAsync(int projectId)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Activity>> GetActivityByUserAsync(int userId)
    {
        throw new System.NotImplementedException();
    }
    public async Task<int> CreateActivityAsync(Activity activity)
    {
        var insertQuery = @"
        INSERT [dbo].[ACTIVITIES]
        ([project_id], [user_id], [activity_type], [description])
        VALUES(@project_id,@user_id,@activity_type,@description)
        ";

        var parameters = new { activity.project_id, activity.user_id, activity.activity_type, activity.description };
        var result = await _dbConnection.QueryFirstOrDefaultAsync<int>(insertQuery, parameters);
        activity.id = result;
        
        return result;
    }

    public Task<Activity> UpdateActivityAsync(Activity activity)
    {
        throw new System.NotImplementedException();
    }
    public Task<bool> DeleteActivityAsync(int id)
    {
        throw new System.NotImplementedException();
    }
}