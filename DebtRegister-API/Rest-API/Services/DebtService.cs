using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rest_API.Services.Interfaces;

namespace Rest_API.Services
{
    public class DebtService : BaseService, IDebtService
    {
        private readonly IMapper _mapper;
        private readonly IDebtRepository _debtRepository;
        public DebtService(IMapper mapper, IDebtRepository debtRepository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(userManager, httpContextAccessor)
        {
            _mapper = mapper;
            _debtRepository = debtRepository;
        }

        public async Task<List<DebtForTable>> GetAllBorrowedDebtsAsync() => _mapper.Map<List<DebtForTable>>(await _debtRepository.GetAllBorrowedDebtsAsync((await GetCurrentUserAsync()).Id));

        public async Task<List<DebtForTable>> GetLastBorrowedDebtsAsync() => _mapper.Map<List<DebtForTable>>(await _debtRepository.GetLastBorrowedDebtsAsync((await GetCurrentUserAsync()).Id));

        public async Task<List<DebtToOrFromForTable>> GetAllBorrowedDebtsFromLenderAsync(int lenderId, bool isLocal) =>
            _mapper.Map<List<DebtToOrFromForTable>>(await _debtRepository.GetAllBorrowedDebtsFromLenderAsync((await GetCurrentUserAsync()).Id, isLocal, lenderId));

        public async Task<List<DebtForTable>> GetAllLentDebtsAsync() =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetAllLentDebtsAsync((await GetCurrentUserAsync()).Id));

        public async Task<List<DebtForTable>> GetLastLentDebtsAsync() => _mapper.Map<List<DebtForTable>>(await _debtRepository.GetLastLentDebtsAsync((await GetCurrentUserAsync()).Id));

        public async Task<List<DebtToOrFromForTable>> GetAllLentDebtsToBorrowerAsync(int borrowerId, bool isLocal) =>
            _mapper.Map<List<DebtToOrFromForTable>>(await _debtRepository.GetAllLentDebtsToBorrowerAsync((await GetCurrentUserAsync()).Id, isLocal, borrowerId));

        public async Task<ViewDebt> GetDebtForViewByIdAsync(int debtId) => _mapper.Map<ViewDebt>(await _debtRepository.GetByIdAsync(debtId));

        public async Task AddDebtAsync(AddBorrowedDebt addBorrowedDebt) => await _debtRepository.CreateAsync(_mapper.Map<Debt>(addBorrowedDebt));

        public async Task AddDebtAsync(AddLentDebt addLentDebt) => await _debtRepository.CreateAsync(_mapper.Map<Debt>(addLentDebt));

        public async Task DeleteDebtAsync(int debtId) => await _debtRepository.DeleteAsync(debtId);
    }
}
