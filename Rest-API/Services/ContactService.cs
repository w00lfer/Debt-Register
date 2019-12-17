using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using Rest_API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Services
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public ContactService(IMapper mapper, IContactRepository contactRepository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(userManager, httpContextAccessor)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<List<ContactForTable>> GetAllContactsAsync()
        {
            var currentUserId = (await GetCurrentUserAsync()).Id;
            return _mapper.Map<List<ContactForTable>>(_contactRepository.GetAll().Where(x => x.CreatorId == currentUserId));
        }

        public async Task<List<LenderOrBorrowerForTable>> GetAllContactsNamesAsync()
        {
            var currentUserId = (await GetCurrentUserAsync()).Id;
            return _mapper.Map<List<LenderOrBorrowerForTable>>(_contactRepository.GetAll().Where(model => model.CreatorId == currentUserId));
        }

        public async Task<Contact> GetContactByIdAsync(int contactId) => await _contactRepository.GetByIdAsync(contactId);

        public async Task CreateContactAsync(AddContact addContact) => await _contactRepository.CreateAsync(_mapper.Map<Contact>(addContact));

        public async Task DeleteContactAsync(int contactId) => await _contactRepository.DeleteAsync(contactId);

        public async Task EditContactAsync(EditContact editContact)
        {
            await _contactRepository.UpdateAsync(_mapper.Map<Contact>(editContact));
        }

        public async Task<string> GetContactFullNameAsync(int contactId) => (await _contactRepository.GetByIdAsync(contactId)).FullName;
    }
}
