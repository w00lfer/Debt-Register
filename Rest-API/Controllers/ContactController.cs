using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Rest_API.Models.DTOs;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        } 

        [HttpGet]
        [Route("{userId}/ContactsFullNames")]
        public async Task<Object> GetAllContactsNamesAsync(int userId) => 
             _mapper.Map<List<LenderOrBorrowerForTable>>(await _contactRepository.GetAllContactsAsync(userId));

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
