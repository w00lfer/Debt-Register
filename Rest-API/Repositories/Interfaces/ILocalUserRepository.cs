using Rest_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface ILocalUserRepository
    {
        Task<IEnumerable<LocalUser>> GetAllLocalUsers();
        Task<LocalUser> GetLocalUserByFullName(string localUserFullname);
        Task CreateLocalUser(LocalUser localUser);
        Task EditLocalUser(LocalUser localUser);
        Task DeleteLocaluser(LocalUser localUser);

    }
}
