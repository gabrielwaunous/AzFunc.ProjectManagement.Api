using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITimeTrackingRepository
{
    Task<IEnumerable<TimeTracking>> GetTimeByUserId(int userId);
    Task<IEnumerable<TimeTracking>> GetTimeByTaskId(int taskId);
    Task<IEnumerable<TimeTracking>> GetAll();
    Task<int> CreateTime(TimeTracking timeTracking);
    Task UpdateTimeTracking(TimeTracking timeTracking);
    Task DeleteTimeTracking(int id);
}