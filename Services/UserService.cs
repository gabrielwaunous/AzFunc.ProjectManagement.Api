using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PersonalProjects.Function.Repositories;

namespace PersonalProjects.Function.Services
{
    public class UserService(ILogger<UserService> log, IUserRepository repository) : IUserService
    {
        private readonly ILogger<UserService> _logger = log;
        private readonly IUserRepository _repository = repository;
        public Task CreateEntityAsync(User user)
        {
            Console.WriteLine($"{user}");
            return _repository.CreateAsync(user);
        }

        public async Task DeleteEntityAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllEntitiesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<User> GetEntityByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateEntityAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}