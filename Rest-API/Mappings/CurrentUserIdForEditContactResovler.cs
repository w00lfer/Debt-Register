using AutoMapper;
using Microsoft.AspNetCore.Http;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rest_API.Mappings
{
    public class CurrentUserIdForEditContactResolver : IValueResolver<EditContact, Contact, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserIdForEditContactResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Resolve(EditContact editContact, Contact contact, int creatorId, ResolutionContext context) =>
            Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}
