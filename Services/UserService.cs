using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalProjects.Function.Repositories;

namespace PersonalProjects.Function.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        private readonly IUserRepository _repository = repository;
        public Task CreateEntityAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllEntitiesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<User> GetEntityByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateEntityAsync(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}