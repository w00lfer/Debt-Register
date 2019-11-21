using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _appDbContext;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public UserRepository(AppDbContext appDbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userManager.Users.ToListAsync();
        public async Task<User> GetUserByIdAsync(int userId) => await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        public async Task<IdentityResult> CreateUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);
    }
}
