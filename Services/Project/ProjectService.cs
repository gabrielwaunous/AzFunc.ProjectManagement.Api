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

    public async Task<bool> DeleteProjectAsync(int id)
    {
        return await _projectRepository.DeleteProjectAsync(id);
    }


    public async Task<Project> UpdateProjectAsync(Project project)
    {
        return await _projectRepository.UpdateProjectAsync(project);
    }
}