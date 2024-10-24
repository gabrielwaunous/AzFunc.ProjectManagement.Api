using System.Collections.Generic;
using System.Threading.Tasks;

public class ProjectRepository : IProjectRepository
{
    public Task<int> CreateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> DeleteProjectAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetAllProjectsByUserAsync(int userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Project> GetProjectByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> UpdateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }
}