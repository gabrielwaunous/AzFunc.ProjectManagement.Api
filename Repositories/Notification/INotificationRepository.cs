using System.Collections.Generic;
using System.Threading.Tasks;

public interface INotificationRepository
{
    public Task<Notification> GetNotificacionById(int id);
    public Task<IEnumerable<Notification>> GetNotificacionByUser(int userId);
    public Task<Notification> CreateNotification(Notification notification);
    public Task<Notification> UpdateNotification(Notification notification);
    public Task<bool> DeleteNotification(int id);
}