using AutoMapper;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace Rest_API.Mappings
{
    public class FullNameForTableResolver : IValueResolver<Debt, DebtForTable, string>
    {
        private readonly IUserService _userService;
        private readonly IContactService _contactService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FullNameForTableResolver(IContactService contactService, IHttpContextAccessor httpContextAccessor)
        {
            _contactService = contactService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Debt debt, DebtForTable debtForTable, string fullName, ResolutionContext context) =>
            !debt.IsLenderLocal
            && int.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var lenderId)
            && lenderId == debt.LenderId
                ? GetBorrowerFullName(debt.IsBorrowerLocal, debt.BorrowerId)
                : GetLenderFullName(debt.IsLenderLocal, debt.LenderId);

        private string GetBorrowerFullName(bool isBorrowerLocal, int borrowerId) => isBorrowerLocal
            ? _contactService.GetContactFullNameAsync(borrowerId).Result
            : _userService.GetUserFullNameAsync(borrowerId).Result;
        private string GetLenderFullName(bool isLenderLocal, int lenderId) => isLenderLocal
            ? _contactService.GetContactFullNameAsync(lenderId).Result
            : _userService.GetUserFullNameAsync(lenderId).Result;
    }
}
