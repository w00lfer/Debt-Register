using AutoMapper;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using System.Linq;
using System.Security.Claims;

namespace Rest_API.Mappings
{
    public class CurrentUseridForAddBorrowedDebtResolver : IValueResolver<AddBorrowedDebt, Debt, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUseridForAddBorrowedDebtResolver(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public int Resolve(AddBorrowedDebt addBorrowedDebt, Debt debt, int borrowerId, ResolutionContext context) =>
            int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}
