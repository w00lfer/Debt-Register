using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API.Models;

namespace Rest_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
         Task CreateUser(UserModel userModel);
    }
}
