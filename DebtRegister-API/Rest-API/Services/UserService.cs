using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        public UserService(IMapper mapper, SignInManager<User> signInManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) :base(userManager, httpContextAccessor)
        {
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<JwtToken> SignUpUserAsync(SignUpUser signUpUser)
        {
            var user = _mapper.Map<User>(signUpUser);
            if (await _userManager.FindByNameAsync(user.UserName) == null)
                if ((await _userManager.CreateAsync(user, signUpUser.Password)).Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return GetTokenForSignedInUser(user);
                }
                else throw new Exception("Can't create user, username, email or password are invalid");
            else throw new Exception("Such a user already exist");
        }

        public async Task<JwtToken> SignInUserAsync(SignInUser signInUser) => await _userManager.FindByNameAsync(signInUser.UserName) is User user ?
            (await _signInManager.PasswordSignInAsync(signInUser.UserName, signInUser.Password, false, true)).Succeeded ?
                GetTokenForSignedInUser(user) :
                throw new Exception("Password is incorrect") :
            throw new Exception("Such a user with this username doesn't exist");

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        public async Task ChangeUserPassword(ChangePassword changePassword)
        {
            var result = await _userManager.ChangePasswordAsync(await GetCurrentUserAsync(), changePassword.CurrentPassword, changePassword.NewPassword);
            if (!result.Succeeded) throw new Exception(result.Errors.ToString());
        }

        public async Task ChangeUserFullNameAsync(string userFullName)
        {
            var currentUser = await GetCurrentUserAsync();
            currentUser.FullName = userFullName;
            var result = await _userManager.UpdateAsync(currentUser);
            if(!result.Succeeded) throw new Exception(result.Errors.ToString());
        }

        public async Task ChangeUserPhoneNumberAsync(string userPhoneNumber)
        {
            var currentUser = await GetCurrentUserAsync();
            currentUser.PhoneNumber = userPhoneNumber;
            var result = await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded) throw new Exception(result.Errors.ToString());
        }

        public async Task<UserInfoForProfile> GetUserInfoForProfileAsync() => _mapper.Map<UserInfoForProfile>(await GetCurrentUserAsync());

        public async Task<List<LenderOrBorrowerForTable>> GetAllUsersExceptCurrentForTableAsync()
        {
            int currentUserId = (await GetCurrentUserAsync()).Id;
            return _mapper.Map<List<LenderOrBorrowerForTable>>(await _userManager.Users.AsQueryable().Where(user => user.Id != currentUserId).ToListAsync());
        }

        public async Task<string> GetUserFullNameAsync(int userId) => (await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId)).FullName;

        private JwtToken GetTokenForSignedInUser(User user)
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var authSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom Secret key for authentication"));

            var token = new JwtSecurityToken(
                issuer: "http://dotnetdetail.net",
                audience: "http://dotnetdetail.net",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo
            };
        }
    }
}
