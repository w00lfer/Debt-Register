using AutoMapper;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Mappings
{
    public class FullnameResolver : IValueResolver<Debt, DebtForTable, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;

        public FullnameResolver(IUserRepository userRepository, IContactRepository contactRepository)
        {
            _userRepository = userRepository;
            _contactRepository = contactRepository;
        }

        public string Resolve(Debt debt, DebtForTable debtForTable, string fullname, ResolutionContext context) => debt.IsLenderLocal
                ? _contactRepository.GetContactByIdAsync(debt.LenderId).Result.FullName
                : _userRepository.GetUserByIdAsync(debt.LenderId).Result.FullName;
    }
}
