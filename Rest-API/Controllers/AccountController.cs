using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Claims;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly  IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _mapper = mapper;
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

        [HttpGet]
        [Route("UsersFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllUsersFullNamesAsync() =>
            _mapper.Map<List<LenderOrBorrowerForTable>>(await _userRepository.GetAllUsersAsync());
    }
}