using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userManager.Users.ToListAsync();
        public async Task<SignUpUser> GetUserByIdAsync(int userId)
        {
            var userEntity = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var user = new SignUpUser
            {
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                FullName = userEntity.FullName
            };
            return user;
        }
        public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);
    }
}
