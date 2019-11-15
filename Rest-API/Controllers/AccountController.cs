using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IUserRepository _userRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> PostUserAsync(SignUpUser signUpUser)
        {
            try
            {
                var user = new User()
                {
                    UserName = signUpUser.UserName,
                    Email = signUpUser.Email,
                    FullName = signUpUser.FullName
                };
                var result = await _userRepository.CreateUserAsync(user, signUpUser.Password);
                if (result.Succeeded) await _signInManager.SignInAsync(user, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
          /*  var user = new User() {
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
            } */
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