using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtController : ControllerBase
    {
        private readonly IDebtRepository _debtRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public DebtController(IDebtRepository debtRepository, IContactRepository contactRepository, IUserRepository userRepository, IMapper mapper)
        {
            _debtRepository = debtRepository;
            _contactRepository = contactRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId}/Borrowed")]
        public async Task<List<DebtForTable>> GetAllBorrowedDebtsAsync(int userId) =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetAllBorrowedDebtsAsync(userId));

        [HttpGet]
        [Route("{userId}/LastBorrowed")]
        public async Task<List<DebtForTable>> GetLastBorrowedDebtsAsync(int userId) =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetLastBorrowedDebtsAsync(userId));

        [HttpGet]
        [Route("{userId}/BorrowedFromLender/{lenderId}/{isLocal}")] 
        public async Task<List<DebtToOrFromForTable>> GetAllBorrowedDebtsFromLenderAsync(int userId, int lenderId, bool isLocal) =>
            _mapper.Map<List<DebtToOrFromForTable>>(await _debtRepository.GetAllBorrowedDebtsFromLenderAsync(userId, isLocal, lenderId));

        [HttpGet]
        [Route("{userId}/Lent")]
        public async Task<List<DebtForTable>> GetAllLentDebtsAsync(int userId) =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetAllLentDebtsAsync(userId));

        [HttpGet]
        [Route("{userId}/LastLent")]
        public async Task<List<DebtForTable>> GetLastLentDebtsAsync(int userId) =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetLastLentDebtsAsync(userId));

        [HttpGet]
        [Route("{userId}/LentToBorrower/{borrowerId}/{isLocal}")]
        public async Task<List<DebtToOrFromForTable>> GetAllLentDebtsToBorrowerAsync(int userId, bool isLocal, int borrowerId) =>
            _mapper.Map<List<DebtToOrFromForTable>>(await _debtRepository.GetAllLentDebtsToBorrowerAsync(userId, isLocal, borrowerId));


        [HttpGet]
        [Route("{debtId}")]
        public async Task<Debt> GetDebtByIdAsync(int debtId) =>
            await _debtRepository.GetDebtByIdAsync(debtId);

        [HttpPost]
        [Route("AddDebt")]
        public async Task AddDebtAsync(Debt debt) =>
            await _debtRepository.CreateDebtAsync(debt);
        [HttpPut]
        [Route("{debtId}")]
        public async Task EditDebtAsync(Debt debt) =>
            await _debtRepository.EditDebtAsync(debt);

        [HttpDelete]
        [Route("{debtId}")]
        public async Task DeleteDebtAsync(Debt debt) =>
            await _debtRepository.DeleteDebtAsync(debt);


    }
}