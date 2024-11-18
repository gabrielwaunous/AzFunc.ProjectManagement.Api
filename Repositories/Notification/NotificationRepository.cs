using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

public class NotificationRepository(IDbConnection dbConnection) : INotificationRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;
    public Task<IEnumerable<Notification>> GetNotificacionByUser(int userId)
    {
        throw new System.NotImplementedException();
    }
    public async Task<Notification> GetNotificacionById(int id)
    {
        var query = @"
            SELECT [id]
                ,[user_id]
                ,[message]
                ,[read]
            FROM [ProjectManagementDB].[dbo].[NOTIFICATIONS]
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