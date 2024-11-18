using System.Collections.Generic;
using System.Threading.Tasks;

public interface INotificationService
{
     Task<Notification> GetNotificacionById(int id);
     Task<IEnumerable<Notification>> GetNotificacionByUser(int userId);
     Task<Notification> CreateNotification(Notification notification);
     Task<Notification> UpdateNotification(Notification notification);
     Task<bool> DeleteNotification(int id);
}