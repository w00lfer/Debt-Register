using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository) => _contactRepository = contactRepository;

        [HttpGet]
        [Route("{userId}/Contact")]
        public async Task<Object> GetAllContactsAsync(int userId) => await _contactRepository.GetAllContactsAsync(userId);

        [HttpGet]
        [Route("{contactId}")]
        public async Task<Object> GetContactByIdAsync(int contactId) => await _contactRepository.GetContactByIdAsync(contactId);

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
