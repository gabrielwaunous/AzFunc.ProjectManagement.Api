using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

public class NotificationRepository(IDbConnection dbConnection) : INotificationRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;
    public async Task<IEnumerable<Notification>> GetNotificacionByUser(int userId)
    {
        var query = @"
            SELECT [id]
                ,[user_id]
                ,[message]
                ,[read]
            FROM [dbo].[NOTIFICATIONS]
            WHERE [user_id] = @user_id
        ";

        var notification  = await _dbConnection.QueryAsync<Notification>(query, new { user_id = userId });
        return notification;
    }
    public async Task<Notification> GetNotificacionById(int id)
    {
        var query = @"
            SELECT [id]
                ,[user_id]
                ,[message]
                ,[read]
            FROM [dbo].[NOTIFICATIONS]
            WHERE [id] = @id
        ";

        var notification  = await _dbConnection.QueryFirstOrDefaultAsync<Notification>(query, new { id = id });
        return notification;
    }
    public Task<Notification> CreateNotification(Notification notification)
    {
        throw new System.NotImplementedException();
    }

    public Task<Notification> UpdateNotification(Notification notification)
    {
        throw new System.NotImplementedException();
    }
    public Task<bool> DeleteNotification(int id)
    {
        throw new System.NotImplementedException();
    }
}