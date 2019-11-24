using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;

namespace Rest_API.Mappings
{
    public class FullNameForTableResolver : IValueResolver<Debt, DebtForTable, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FullNameForTableResolver(IUserRepository userRepository, IContactRepository contactRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _contactRepository = contactRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Debt debt, DebtForTable debtForTable, string fullName, ResolutionContext context) =>
            !debt.IsLenderLocal
            && int.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var lenderId)
            && lenderId == debt.LenderId
                ? GetBorrowerFullName(debt.IsBorrowerLocal, debt.BorrowerId)
                : GetLenderFullName(debt.IsLenderLocal, debt.LenderId);
        private string GetBorrowerFullName(bool isBorrowerLocal, int borrowerId) => isBorrowerLocal
            ? _contactRepository.GetContactByIdAsync(borrowerId).Result.FullName
            : _userRepository.GetUserByIdAsync(borrowerId).Result.FullName;
        private string GetLenderFullName(bool isLenderLocal, int lenderId) => isLenderLocal
            ? _contactRepository.GetContactByIdAsync(lenderId).Result.FullName
            : _userRepository.GetUserByIdAsync(lenderId).Result.FullName;
    }
}
