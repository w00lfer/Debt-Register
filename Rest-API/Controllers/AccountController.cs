using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) => _userService = userService;

                [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(SignInUser signInUser) => Ok(await _userService.SignInUserAsync(signInUser));

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(SignUpUser signUpUser) => Ok(await _userService.SignUpUserAsync(signUpUser));

        [HttpPost]
        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok("You have logout successfuly");
        }

        [HttpGet]
        [Authorize]
        [Route("UsersFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllUsersFullNamesAsync() =>
           await _userService.GetAllUsersExceptCurrentForTableAsync();

        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangeUserPassword(ChangePassword changePassword)
        {
            await _userService.ChangeUserPassword(changePassword);
            return Ok("You have changed password successfully");
        }

        [HttpPost]
        [Authorize]
        [Route("ChangeUserFullNameAsync")]
        public async Task<IActionResult> ChangeUserFullName([FromForm] string userFullName)
        {
            await _userService.ChangeUserFullNameAsync(userFullName);
            return Ok("You have changed fullname successfully");
        }

        [HttpPost]
        [Authorize]
        [Route("ChangePhoneNumber")]
        public async Task<IActionResult> ChangeUserPhoneNumber([FromForm] string userPhoneNumber)
        {
            await _userService.ChangeUserPhoneNumberAsync(userPhoneNumber);
            return Ok("You have changed fullname successfully");
        }

        [HttpGet]
        [Authorize]
        [Route("UserProfile")]
        public async Task<UserInfoForProfile> GetUserInfoForProfile() =>
            await _userService.GetUserInfoForProfileAsync();
    }
}