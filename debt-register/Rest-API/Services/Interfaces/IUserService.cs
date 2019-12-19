using Rest_API.Models;
using Rest_API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<LenderOrBorrowerForTable>> GetAllUsersExceptCurrentForTableAsync();
        Task<JwtToken> SignUpUserAsync(SignUpUser signUpUser);
        Task<JwtToken> SignInUserAsync(SignInUser signInUser);
        Task LogoutAsync();
        Task ChangeUserPassword(ChangePassword changePassword);
        Task ChangeUserFullNameAsync(string userFullName);
        Task ChangeUserPhoneNumberAsync(string userPhoneNumber);
        Task<UserInfoForProfile> GetUserInfoForProfileAsync();
        Task<string> GetUserFullNameAsync(int userId);
    }
}
