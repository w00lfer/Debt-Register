﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using System.Linq;
using System.Security.Claims;

namespace Rest_API.Mappings
{
    public class CurrentUseridForAddLentDebtResolver : IValueResolver<AddLentDebt, Debt, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUseridForAddLentDebtResolver(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public int Resolve(AddLentDebt addLentDebt, Debt debt, int lenderId, ResolutionContext context) =>
            int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}
