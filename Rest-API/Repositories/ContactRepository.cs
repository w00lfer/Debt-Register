using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private AppDbContext _appDbContext;
        public ContactRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<IEnumerable<Contact>> GetAllContactsAsync(int creatorId) => await _appDbContext.Contacts.Where(c => c.CreatorId == creatorId).ToListAsync();

        public async Task<Contact> GetContactByIdAsync(int contactId) => await _appDbContext.Contacts.Where(c => c.Id == contactId).FirstOrDefaultAsync();
        public async Task CreateContactAsync(Contact contact)
        {
            await _appDbContext.Contacts.AddAsync(contact);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            _appDbContext.Contacts.Remove(contact);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task EditContactAsync(Contact contact)
        {
            _appDbContext.Contacts.Update(contact);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
