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
using Rest_API.Services.Interfaces;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactSerivce) => _contactService = contactSerivce;


        [HttpGet]
        public async Task<List<ContactForTable>> GetAllContactsAsync() =>
            await _contactService.GetAllContactsAsync();

        [HttpGet]
        [Route("ContactsFullNames")]
        public async Task<List<LenderOrBorrowerForTable>> GetAllContactsNamesAsync() =>
            await _contactService.GetAllContactsNamesAsync();
                
        [HttpGet]
        [Route("{contactId}")]
        public async Task<Contact> GetContactByIdAsync(int contactId) =>
            await _contactService.GetContactByIdAsync(contactId);

        [HttpPut]
        [Route("{contactId}")]
        public async Task<IActionResult> EditContactAsync(EditContact editContact)
        {
            await _contactService.EditContactAsync(editContact);
            return Ok("You have edited contact successfully");
        }


        [HttpDelete]
        [Route("{contactId}")]
        public async Task<IActionResult> DeleteContactAsync(int contactId)
        {
            await _contactService.DeleteContactAsync(contactId);
            return Ok();
        }

        // TO DO CREATION OF CONTACT WITH VALIDATION ON FULLNAME TO BE NOT THE SAME AS OTHER ONE FOR SAME USER!
        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> CreateContactAsync(AddContact addContact)
        {
            await _contactService.CreateContactAsync(addContact);
            return Ok();
        }

        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
