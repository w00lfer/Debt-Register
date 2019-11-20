using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Rest_API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, UserManager<User> userManager, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ContactsFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllContactsNamesAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return _mapper.Map<List<LenderOrBorrowerForTable>>(await _contactRepository.GetAllContactsAsync(currentUser.Id));
        }
                
        [HttpGet]
        [Route("{contactId}")]
        public async Task<Contact> GetContactByIdAsync(int contactId) => await _contactRepository.GetContactByIdAsync(contactId);

        [HttpPut]
        [Route("{contactId}")]
        public async Task EditContactAsync(Contact contact) => await _contactRepository.EditContactAsync(contact);

        [HttpDelete]
        [Route("{contactId}")]
        public async Task DeleteContactAsync(Contact contact) => await _contactRepository.DeleteContactAsync(contact);
        
        [HttpPost]
        [Route("AddContact")]
        public async Task CreateContactAsync(Contact contact) => await _contactRepository.CreateContactAsync(contact);

        
    }
}
