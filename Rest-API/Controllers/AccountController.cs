using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Authorize]
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
        [Route("Logout")]
        public async Task<Object> LogoutAsync( )
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("UsersFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllUsersFullNamesAsync() =>
            _mapper.Map<List<LenderOrBorrowerForTable>>(await _userRepository.GetAllUsersExceptCurrentAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)));

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangeUserPassword(ChangePassword changePassword)
        {
            User currentUser = await _userManager.GetUserAsync(User);
            await _userManager.ChangePasswordAsync(currentUser, changePassword.CurrentPassword, changePassword.NewPassword);
            return Ok();
        }

        [HttpPost]
        [Route("ChangeUserFullName")]
        public async Task<IActionResult> ChangeUserFullName([FromForm] string userFullName)
        {
            User currentUser = await _userManager.GetUserAsync(User);
            currentUser.FullName = userFullName;
            await _userManager.UpdateAsync(currentUser);
            return Ok();
        }

        [HttpPost]
        [Route("ChangePhoneNumber")]
        public async Task<IActionResult> ChangeUserPhoneNumber([FromForm] string userPhoneNumber)
        {
            User currentUser = await _userManager.GetUserAsync(User);
            currentUser.PhoneNumber = userPhoneNumber;
            await _userManager.UpdateAsync(currentUser);
            return Ok();
        }

        [HttpGet]
        [Route("UserProfile")]
        public async Task<UserInfoForProfile> GetUserInfoForProfile() =>
            _mapper.Map<UserInfoForProfile>(await _userRepository.GetUserByIdAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)));
        }
}