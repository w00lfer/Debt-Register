using AutoMapper;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;

namespace Rest_API.Mappings
{
    public class MappingProfile : Profile
    {
        //public MappingProfile(IContactRepository contactRepository, IUserRepository userRepository)
        //{
        //    CreateMap<Debt, DebtForTable>()
        //        .ForMember(dest => dest.ContactFullName,
        //        opts => opts.MapFrom(debt => debt.IsLenderLocal
        //        ? contactRepository.GetContactByIdAsync(debt.LenderId).Result.FullName
        //        : userRepository.GetUserByIdAsync(debt.LenderId).Result.FullName));
        //    CreateMap<Debt, DebtToOrFromForTable>();
        //}
        public MappingProfile()
        {
            CreateMap<Debt, DebtForTable>();
            CreateMap<Debt, DebtToOrFromForTable>();
        }
    }
}
