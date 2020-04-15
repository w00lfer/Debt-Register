using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         private readonly IUserService _userService;

        public AuthController(IUserService userService) => _userService = userService;

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
    }
}