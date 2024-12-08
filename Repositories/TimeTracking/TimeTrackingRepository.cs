using System.Collections.Generic;
using System.Threading.Tasks;

class TimeTrackingRepository : ITimeTrackingRepository
{

    public Task<IEnumerable<TimeTracking>> GetAll()
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<TimeTracking>> GetTimeByTaskId(int taskId)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<TimeTracking>> GetTimeByUserId(int userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> CreateTime(TimeTracking timeTracking)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateTimeTracking(TimeTracking timeTracking)
    {
        throw new System.NotImplementedException();
    }
    public Task DeleteTimeTracking(int id)
    {
        throw new System.NotImplementedException();
    }
}