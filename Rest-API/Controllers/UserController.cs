using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using System;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> PostUserAsync(SignUpUser signUpUser)
        {
            var user = new User() {
                UserName = signUpUser.UserName,
                Email = signUpUser.Email,
                FullName = signUpUser.FullName
            };
                
            try
            {
                var result = await _userManager.CreateAsync(user, signUpUser.Password);
                if (result.Succeeded) await _signInManager.SignInAsync(user, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        //Post : /api/User/Login
        public async Task<Object> LoginAsync(SignInUser signInUser)
        {
            var result = await _signInManager.PasswordSignInAsync(signInUser.UserName, signInUser.Password, false, true); 
            return Ok(result);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<Object> LogoutAsync(SignInUser signInUser)
        {
            throw new NotImplementedException();
        }
    }
}