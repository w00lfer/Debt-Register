using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories
{
    public class LocalUserRepository : ILocalUserRepository
    {
        private AppDbContext _appDbContext;
        public LocalUserRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public async Task<IEnumerable<LocalUser>> GetAllLocalUsers() => await _appDbContext.LocalUsers.ToListAsync();
        public async Task<LocalUser> GetLocalUserByFullName(string localUserFullName) => await _appDbContext.LocalUsers.FirstOrDefaultAsync(x => x.FullName == localUserFullName);
        public async Task CreateLocalUser(LocalUser localUser)
        {
            await _appDbContext.LocalUsers.AddAsync(localUser);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteLocaluser(LocalUser localUser)
        {
            _appDbContext.LocalUsers.Remove(localUser);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task EditLocalUser(LocalUser localUser)
        {
            _appDbContext.LocalUsers.Update(localUser);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
