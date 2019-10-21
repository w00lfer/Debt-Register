using Rest_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<UserModel> GetUserById(string userId);
         Task CreateUser(UserModel userModel);
    }
}
