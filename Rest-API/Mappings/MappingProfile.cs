using AutoMapper;
using Rest_API.Models;
using Rest_API.Models.DTOs;

namespace Rest_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Debt, DebtForTable>()
                .ForMember(dest => dest.ContactFullName, opts => opts.MapFrom<FullNameResolver>());
            CreateMap<Debt, DebtToOrFromForTable>();
            CreateMap<User, LenderOrBorrowerForTable>();
            CreateMap<Contact, LenderOrBorrowerForTable>();
            CreateMap<AddBorrowedDebt, Debt>()
                .ForMember(dest => dest.BorrowerId, opts => opts.MapFrom<CurrentUseridForAddBorrowedDebtResolver>())
                .ForMember(dest => dest.IsBorrowerLocal, opts => opts.MapFrom(src => false)); // becuase in this case user is always borrower
            CreateMap<AddLentDebt, Debt>()
                .ForMember(dest => dest.LenderId, opts => opts.MapFrom<CurrentUseridForAddLentDebtResolver>())
                .ForMember(dest => dest.IsLenderLocal, opts => opts.MapFrom(src => false)); // because in this case user is always lender
            CreateMap<User, UserInfoForProfile>();
            CreateMap<Contact, ContactForTable>();
            CreateMap<AddContact, Contact>()
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom<CurrentUserIdForAddContactResolver>());
            CreateMap<EditContact, Contact>()
                .ForMember(dest => dest.CreatorId, opts => opts.MapFrom<CurrentUserIdForEditContactResolver>());
        }
    }
}
