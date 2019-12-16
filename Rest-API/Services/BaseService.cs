using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;

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
