using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

public class ProjectRepository : IProjectRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
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

    public async Task<Project> GetProjectByIdAsync(int id)
    {
        try
        {
            var query = @"SELECT * FROM Projects WHERE id = @id";
            var project = await _dbConnection.QueryFirstOrDefaultAsync<Project>(query, new { id = id });
            return project;
        }catch(Exception ex)
        {
            throw new Exception("Error retrieving projects.", ex);
        }
    }
    public async Task<int> CreateProjectAsync(Project project)
    {
        var query = @"INSERT INTO Projects(user_id,name, description)
                    VALUES(@user_id,@name,@description);
                    SELECT CAST(SCOPE_IDENTITY() AS int);
        ";
        var parameters = new { project.user_id, project.name, project.description};
        var projectId = await _dbConnection.QueryFirstOrDefaultAsync<int>(query,parameters);
        return projectId;
    }

    public Task<int> DeleteProjectAsync(long id)
    {
        throw new System.NotImplementedException();
    }


    public Task<int> UpdateProjectAsync(Project project)
    {
        throw new System.NotImplementedException();
    }
}