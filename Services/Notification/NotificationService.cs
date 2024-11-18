using System.Collections.Generic;
using System.Threading.Tasks;

public class NotificationService(INotificationRepository repository) : INotificationService
{
    private readonly INotificationRepository _repository = repository;
    public async Task<IEnumerable<Notification>> GetNotificacionByUser(int userId)
        => await _repository.GetNotificacionByUser(userId);
    public async Task<Notification> GetNotificacionById(int id)
        => await _repository.GetNotificacionById(id);
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