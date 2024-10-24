using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using PersonalProjects.Function.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ILogger _logger;

    public UserRepository(IDbConnection dbConnection, ILogger log)
    {
        _dbConnection = dbConnection;
        _logger = log;
    }
    public async Task<User> CreateAsync(User user)
    {
        var query = @"
        INSERT INTO Users (username, email, password)
        VALUES (@username, @email, @password);
        SELECT CAST(SCOPE_IDENTITY() AS int);
    ";

        var parameters = new { user.username, user.email, user.password };
        user.id = await _dbConnection.QueryFirstOrDefaultAsync<int>(query, parameters);

        return user;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var query = "SELECT * FROM Users";
        var users = await _dbConnection.QueryAsync<User>(query);
        return users;
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<User> UpdateAsync(User entity)
    {
        throw new System.NotImplementedException();
    }
}

