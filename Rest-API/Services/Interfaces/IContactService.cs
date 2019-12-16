using Rest_API.Models;
using Rest_API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactForTable>> GetAllContactsAsync();
        Task<List<LenderOrBorrowerForTable>> GetAllContactsNamesAsync();
        Task<Contact> GetContactByIdAsync(int contactId);
        Task EditContactAsync(EditContact editContact);
        Task DeleteContactAsync(int contactId);
        Task CreateContactAsync(AddContact addContact);
        Task<string> GetContactFullNameAsync(int contactId);

    }
}
