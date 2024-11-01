using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalProjects.Function.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllEntitiesAsync();
        Task<User> GetEntityByIdAsync(int id);
        Task CreateEntityAsync(User entity);
        Task UpdateEntityAsync(User entity);
        Task DeleteEntityAsync(int id);
    }
}
