using System.Collections.Generic;
using System.Threading.Tasks;

public class ActivityService (IActivityRepository activityRepository) : IActivityService
{
    private readonly IActivityRepository _activityRepository = activityRepository;
    public async Task<int> CreateActivityAsync(Activity activity)
        => await _activityRepository.CreateActivityAsync(activity);    


    public async Task<Activity> GetActivityByIdAsync(int id)
        => await _activityRepository.GetActivityByIdAsync(id);

    public async Task<IEnumerable<Activity>> GetAllActivitiesByProjectAsync(int projectId)
        => await _activityRepository.GetActivityByProjectAsync(projectId);

    public async Task<IEnumerable<Activity>> GetAllActivitiesByUserAsync(int userId)
        => await _activityRepository.GetActivityByUserAsync(userId);

    public async Task<Activity> UpdateActivityAsync(Activity activity)
        => await _activityRepository.UpdateActivityAsync(activity);
    public async Task<bool> DeleteActivityAsync(int id)
        => await _activityRepository.DeleteActivityAsync(id);
}