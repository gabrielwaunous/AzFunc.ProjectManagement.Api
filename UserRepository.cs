using PersonalProjects.Function.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PersonalProjects.Function
{
	internal class UserReposiatory : IUserRepository
	{
		private readonly string _connectionString = connectionString;

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			using (IDbConnection dbConnection = new SqlConnection(_connectionString))
			{
				await dbConnection.OpenAsync();
				return await dbConnection.QueryAsync<User>("SELECT * FROM User");
			}
		}
	}
}