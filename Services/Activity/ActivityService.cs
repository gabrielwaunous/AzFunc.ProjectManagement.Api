using System.Collections.Generic;
using System.Threading.Tasks;

public class ActivityService : IActivityService
{
    public Task<int> CreateActivityAsync(Activity activity)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteActivityAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<Activity> GetActivityByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Activity>> GetAllActivitiesByProjectAsync(int projectId)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Activity>> GetAllActivitiesByUserAsync(int userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Activity> UpdateActivityAsync(Activity activity)
    {
        throw new System.NotImplementedException();
    }
}