using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

public class ProjectRepository : IProjectRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public Task<int> CreateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> DeleteProjectAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<Project>> GetAllProjectsByUserAsync(int userId)
    {
        try
        {
            var query = "SELECT * FROM Projects WHERE user_id = @UserId"; // Ajusta el parámetro a UserId
            var projects = await _dbConnection.QueryAsync<Project>(query, new { UserId = userId }); // Usa la misma clave en el objeto anónimo
            return projects;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving projects.", ex);
        }
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