using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProjectRepository
{
    public Task<IEnumerable<Project>> GetAllProjectsByUserAsync(int userId);
     public Task<Project> GetProjectByIdAsync(int id);
     public Task<int> CreateProjectAsync(Project project);
     public Task<int> UpdateProjectAsync(Project project);
     public Task<bool> DeleteProjectAsync(int id);
}