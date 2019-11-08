using Rest_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<SignUpUser> GetUserById(int userId);
        Task CreateUser(SignUpUser signUpUser);
    }
}
