using AutoMapper;
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
        public async Task<Object> GetAllBorrowedDebtsAsync(int userId) 
        {
            var borrowedDebts = await _debtRepository.GetAllBorrowedDebtsAsync(userId);
            return _mapper.Map<List<DebtForTable>>(borrowedDebts);
            // TRY TO MAKE THIS SOMEHOW ASYNC??????
            //return borrowedDebts.Select(debt => new DebtForTable
            //{
            //    Id = debt.Id,
            //    Name = debt.Name,
            //    Value = debt.Value,
            //    DebtStartDate = debt.DebtStartDate,
            //    ContactFullName = debt.IsLenderLocal ? _contactRepository.GetContactByIdAsync(debt.LenderId).Result.FullName : _userRepository.GetUserByIdAsync(debt.LenderId).Result.FullName,
            //    IsPayed = debt.IsPayed
            //});
        }

        [HttpGet]
        [Route("{userId}/LastBorrowed")]
        public async Task<Object> GetLastBorrowedDebtsAsync(int userId)
        {
            var lastBorrowedDebts = await _debtRepository.GetLastBorrowedDebtsAsync(userId);
            return _mapper.Map<List<DebtForTable>>(lastBorrowedDebts);
            //return lastBorrowedDebts.Select(debt => new DebtForTable
            //{
            //    Id = debt.Id,
            //    Name = debt.Name,
            //    Value = debt.Value,
            //    DebtStartDate = debt.DebtStartDate,
            //    ContactFullName = debt.IsLenderLocal ? _contactRepository.GetContactByIdAsync(debt.LenderId).Result.FullName : _userRepository.GetUserByIdAsync(debt.LenderId).Result.FullName,
            //    IsPayed = debt.IsPayed
            //});
        }

        [HttpGet]
        [Route("{userId}/BorrowedFromLender/{lenderId}/{isLocal}")] 
        public async Task<Object> GetAllBorrowedDebtsFromLenderAsync(int userId, int lenderId, bool isLocal)
        {          
            var borrowedDebtsFromLender = await _debtRepository.GetAllBorrowedDebtsFromLenderAsync(userId, isLocal, lenderId);
            return _mapper.Map<List<DebtToOrFromForTable>>(borrowedDebtsFromLender);
        }

        [HttpGet]
        [Route("{userId}/Lent")]
        public async Task<Object> GetAllLentDebtsAsync(int userId)
        {
            var lentDebts = await _debtRepository.GetAllLentDebtsAsync(userId);
            return _mapper.Map<List<DebtForTable>>(lentDebts);
            // TRY TO MAKE THIS SOMEHOW ASYNC??????
            //return lentDebts.Select(debt => new DebtForTable
            //{
            //    Id = debt.Id,
            //    Name = debt.Name,
            //    Value = debt.Value,
            //    DebtStartDate = debt.DebtStartDate,
            //    ContactFullName = debt.IsBorrowerLocal ? _contactRepository.GetContactByIdAsync(debt.LenderId).Result.FullName : _userRepository.GetUserByIdAsync(debt.LenderId).Result.FullName,
            //    IsPayed = debt.IsPayed
            //});
        }

        [HttpGet]
        [Route("{userId}/LastLent")]
        public async Task<Object> GetLastLentDebtsAsync(int userId)
        {
            var lastLentDebts = await _debtRepository.GetLastLentDebtsAsync(userId);
            return _mapper.Map<List<DebtForTable>>(lastLentDebts);
            // TRY TO MAKE THIS SOMEHOW ASYNC??????
            //return lastLentDebts.Select(debt => new DebtForTable
            //{
            //    Id = debt.Id,
            //    Name = debt.Name,
            //    Value = debt.Value,
            //    DebtStartDate = debt.DebtStartDate,
            //    ContactFullName = debt.IsBorrowerLocal ? _contactRepository.GetContactByIdAsync(debt.BorrowerId).Result.FullName : _userRepository.GetUserByIdAsync(debt.BorrowerId).Result.FullName,
            //    IsPayed = debt.IsPayed
            //});
        }

        [HttpGet]
        [Route("{userId}/LentToBorrower/{borrowerId}/{isLocal}")]
        public async Task<Object> GetAllLentDebtsToBorrowerAsync(int userId, bool isLocal, int borrowerId)
        {
            var lentDebtsToBorrower = await _debtRepository.GetAllLentDebtsToBorrowerAsync(userId, isLocal, borrowerId);
            return _mapper.Map<List<DebtToOrFromForTable>>(lentDebtsToBorrower);
        }

        [HttpGet]
        [Route("{debtId}")]
        public async Task<Object> GetDebtByIdAsync(int debtId) =>
            await _debtRepository.GetDebtByIdAsync(debtId);

        [HttpPost]
        [Route("AddDebt")]
        public async Task AddDebtAsync(Debt debt) =>
            await _debtRepository.CreateDebtAsync((debt));

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