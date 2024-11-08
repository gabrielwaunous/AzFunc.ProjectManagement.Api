using System.Collections.Generic;
using System.Threading.Tasks;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> GetActivityByProjectAsync(int projectId);
    Task<IEnumerable<Activity>> GetActivityByUserAsync(int userId);
    Task<Activity> GetActivityByIdAsync(int id);
    Task<int> CreateActivityAsync(Activity activity);
    Task<Activity> UpdateActivityAsync(Activity activity);
    Task<bool> DeleteActivityAsync(int id);
}