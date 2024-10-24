using System.Collections.Generic;
using System.Threading.Tasks;

interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsByUserAsync(int userId);
    Task<Project> GetProjectByIdAsync(int id);
    Task<Project> CreateProjectAsync(Project project);
    Task<Project> UpdateProjectAsync(Project project);
    Task<bool> DeleteProjectAsync(int id);
}