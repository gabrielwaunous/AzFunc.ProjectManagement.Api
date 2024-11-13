using System;
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

    public async Task<IEnumerable<Activity>> GetActivityByProjectAsync(int projectId)
    {
        var query = @"
            SELECT [id]
                ,[project_id]
                ,[user_id]
                ,[activity_type]
                ,[description]
            FROM [dbo].[ACTIVITIES]
            WHERE [project_id] = @projectId
        ";

        var activityList = await _dbConnection.QueryAsync<Activity>(query, new { projectId });
        return activityList;
    }

    public async Task<IEnumerable<Activity>> GetActivityByUserAsync(int userId)
    {
        var query = @"
            SELECT [id]
                ,[project_id]
                ,[user_id]
                ,[activity_type]
                ,[description]
            FROM [dbo].[ACTIVITIES]
            WHERE [user_id] = @userId
        ";

        var activityList = await _dbConnection.QueryAsync<Activity>(query, new { userId });
        return activityList;
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

    public async Task<Activity> UpdateActivityAsync(Activity activity)
    {
        var currentActivity = await GetActivityByIdAsync(activity.id);
        if (currentActivity == null)
        {
            throw new Exception("Activity not found");
        }

        bool hasChanges = false;
        var updateFields = new List<string>();

        if (currentActivity.project_id != activity.project_id)
        {
            currentActivity.project_id = activity.project_id;
            updateFields.Add("project_id = @project_id");
            hasChanges = true;
        }

        if (currentActivity.user_id != activity.user_id)
        {
            currentActivity.user_id = activity.user_id;
            updateFields.Add("user_id = @user_id");
            hasChanges = true;
        }

        if (currentActivity.activity_type != activity.activity_type)
        {
            currentActivity.activity_type = activity.activity_type;
            updateFields.Add("activity_type = @activity_type");
            hasChanges = true;
        }

        if (currentActivity.description != activity.description)
        {
            currentActivity.description = activity.description;
            updateFields.Add("description = @description");
            hasChanges = true;
        }

        if (!hasChanges)
        {
            return currentActivity; // No se realizaron cambios
        }

        var query = $"UPDATE Activities SET {string.Join(", ", updateFields)} WHERE id = @id";
        await _dbConnection.ExecuteAsync(query, currentActivity);

        return currentActivity;
    }
    public async Task<bool> DeleteActivityAsync(int id)
    {
        var deleteQuery = @"
        DELETE FROM Activities WHERE id = @id
        ";

        var response = await _dbConnection.ExecuteAsync(deleteQuery, new { id = id });

        return response > 0;
    }
}