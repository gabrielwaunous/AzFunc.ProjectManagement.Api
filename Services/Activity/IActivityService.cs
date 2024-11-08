using System.Collections.Generic;
using System.Threading.Tasks;

public interface IActivityService
{
    Task<IEnumerable<Activity>> GetAllActivitiesByUserAsync(int userId);
    Task<IEnumerable<Activity>> GetAllActivitiesByProjectAsync(int projectId);
    Task<Activity> GetActivityByIdAsync(int id);
    Task<int> CreateActivityAsync(Activity activity);
    Task<Activity> UpdateActivityAsync(Activity activity);
    Task<bool> DeleteActivityAsync(int id);
}