using System.Collections.Generic;
using System.Threading.Tasks;

public class ProjectService(IProjectRepository repository) : IProjectService
{
    private readonly IProjectRepository _projectRepository  =  repository;
    public Task<IEnumerable<Project>> GetAllProjectsByUserAsync(int userId)
    {
        return _projectRepository.GetAllProjectsByUserAsync(userId);
    }

    public Task<Project> GetProjectByIdAsync(int id)
    {
        return _projectRepository.GetProjectByIdAsync(id);
    }
    public async Task<Project> CreateProjectAsync(Project project)
    {
        var projectId = await _projectRepository.CreateProjectAsync(project);
        project.id = projectId;
        return project;

    }

    public Task<bool> DeleteProjectAsync(int id)
    {
        throw new System.NotImplementedException();
    }


    public Task<Project> UpdateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }
}