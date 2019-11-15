using Rest_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync(int creatorId);
        Task<Contact> GetContactByIdAsync(int contactId);
        Task CreateContactAsync(Contact contact);
        Task EditContactAsync(Contact contact);
        Task DeleteContactAsync(Contact contact);
    }
}
