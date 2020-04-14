using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService) => _contactService = contactService;


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
            return Ok("You have deleted contact successfully");
        }

        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> CreateContactAsync(AddContact addContact)
        {
            await _contactService.CreateContactAsync(addContact);
            return Ok("You have added contact successfully");
        }
    }
}
