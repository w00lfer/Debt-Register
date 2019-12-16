using AutoMapper;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using System;
using System.Linq;
using System.Security.Claims;

namespace Rest_API.Mappings
{
    public class CurrentUserIdForAddContactResolver : IValueResolver<AddContact, Contact, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserIdForAddContactResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Resolve(AddContact addContact, Contact contact, int creatorId, ResolutionContext context) =>
            Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}
