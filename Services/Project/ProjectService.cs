using System.Collections.Generic;
using System.Threading.Tasks;

class ProjectService : IProjectService
{
    public Task<Project> CreateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteProjectAsync(int id)
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

    public Task<Project> UpdateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }
}