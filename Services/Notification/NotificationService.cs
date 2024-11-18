using System.Collections.Generic;
using System.Threading.Tasks;

public class NotificationService : INotificationService
{
    public Task<Notification> CreateNotification(Notification notification)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteNotification(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<Notification> GetNotificacionById(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Notification>> GetNotificacionByUser(int userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Notification> UpdateNotification(Notification notification)
    {
        throw new System.NotImplementedException();
    }
}