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
        public async Task<List<ContactForTable>> GetAllContactsAsync() =>
            _mapper.Map<List<ContactForTable>>(await _contactRepository.GetAllContactsAsync(GetCurrentUserId()));

        [HttpGet]
        [Route("ContactsFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllContactsNamesAsync() =>
            _mapper.Map<List<LenderOrBorrowerForTable>>(await _contactRepository.GetAllContactsAsync(GetCurrentUserId()));
                
        [HttpGet]
        [Route("{contactId}")]
        public async Task<Contact> GetContactByIdAsync(int contactId) => await _contactRepository.GetContactByIdAsync(contactId);

        [HttpPut]
        [Route("{contactId}")]
        public async Task<IActionResult> EditContactAsync(int contactId, EditContact editContact)
        {
            if (contactId != editContact.Id) return BadRequest();
            await _contactRepository.EditContactAsync(_mapper.Map<Contact>(editContact));
            return Ok();
        }


        [HttpDelete]
        [Route("{contactId}")]
        public async Task<IActionResult> DeleteContactAsync(int contactId)
        {
            await _contactRepository.DeleteContactAsync(await _contactRepository.GetContactByIdAsync(contactId));
            return Ok();
        }

        // TO DO CREATION OF CONTACT WITH VALIDATION ON FULLNAME TO BE NOT THE SAME AS OTHER ONE FOR SAME USER!
        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> CreateContactAsync(AddContact addContact)
        {
            await _contactRepository.CreateContactAsync(_mapper.Map<Contact>(addContact));
            return Ok();
        }

        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
