using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly  IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<Object> LogoutAsync(SignInUser signInUser)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpGet]
        [Route("UsersFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllUsersFullNamesAsync() =>
            _mapper.Map<List<LenderOrBorrowerForTable>>(await _userRepository.GetAllUsersAsync());
    }
}