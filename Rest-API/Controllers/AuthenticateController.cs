using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Rest_API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public AuthenticateController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(SignInUser signInUser)
        {
            var user = await _userManager.FindByNameAsync(signInUser.UserName);
            var result = await _signInManager.PasswordSignInAsync(signInUser.UserName, signInUser.Password, false, true);
            if (user != null && result.Succeeded)
            {

                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom Secret key for authnetication"));

                var token = new JwtSecurityToken(
                    issuer: "http://dotnetdetail.net",
                    audience: "http://dotnetdetail.net",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(SignUpUser signUpUser)
        {
            var user = new User()
            {
                UserName = signUpUser.UserName,
                FullName = signUpUser.FullName,
                Email = signUpUser.Email,
            };
            var isUsernameAlreadyTaken = await _userManager.FindByNameAsync(user.UserName) !=null;
            var accountCreationResult = await _userRepository.CreateUserAsync(user, signUpUser.Password);
            if (!isUsernameAlreadyTaken && accountCreationResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom Secret key for authnetication"));

                var token = new JwtSecurityToken(
                    issuer: "http://dotnetdetail.net",
                    audience: "http://dotnetdetail.net",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
