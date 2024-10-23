using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using PersonalProjects.Function.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public Task<User> CreateAsync(User entity)
    {
        throw new System.NotImplementedException();
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

