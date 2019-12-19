using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rest_API.Models;
using System.Threading.Tasks;

namespace Rest_API.Services
{
    public abstract class BaseService
    {
        protected readonly UserManager<User> _userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<User> GetCurrentUserAsync() => await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
    }
}
