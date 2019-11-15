using Microsoft.AspNetCore.Identity;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<SignUpUser> GetUserByIdAsync(int userId);
        Task<IdentityResult> CreateUserAsync(User user, string password);
    }
}
