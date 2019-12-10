﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Models.DTOs;
using Rest_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DebtController : ControllerBase
    {
        private readonly IDebtRepository _debtRepository;
        private readonly IMapper _mapper;
        public DebtController(IDebtRepository debtRepository, IContactRepository contactRepository, IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            _debtRepository = debtRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Borrowed")]
        public async Task<List<DebtForTable>> GetAllBorrowedDebtsAsync() =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetAllBorrowedDebtsAsync(GetCurrentUserId()));

        [HttpGet]
        [Route("LastBorrowed")]
        public async Task<List<DebtForTable>> GetLastBorrowedDebtsAsync() =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetLastBorrowedDebtsAsync(GetCurrentUserId()));
        
        [HttpGet]
        [Route("BorrowedFromLender/{lenderId}/{isLocal}")]
        public async Task<List<DebtToOrFromForTable>> GetAllBorrowedDebtsFromLenderAsync(int lenderId, bool isLocal) =>
            _mapper.Map<List<DebtToOrFromForTable>>(await _debtRepository.GetAllBorrowedDebtsFromLenderAsync(GetCurrentUserId(), isLocal, lenderId));
 

        [HttpGet]
        [Route("Lent")]
        public async Task<List<DebtForTable>> GetAllLentDebtsAsync() =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetAllLentDebtsAsync(GetCurrentUserId()));

        [HttpGet]
        [Route("LastLent")]
        public async Task<List<DebtForTable>> GetLastLentDebtsAsync() =>
            _mapper.Map<List<DebtForTable>>(await _debtRepository.GetLastLentDebtsAsync(GetCurrentUserId()));

        [HttpGet]
        [Route("LentToBorrower/{borrowerId}/{isLocal}")]
        public async Task<List<DebtToOrFromForTable>> GetAllLentDebtsToBorrowerAsync(bool isLocal, int borrowerId) =>
            _mapper.Map<List<DebtToOrFromForTable>>(await _debtRepository.GetAllLentDebtsToBorrowerAsync(GetCurrentUserId(), isLocal, borrowerId));


        [HttpGet]
        [Route("ViewDebt/{debtId}")]
        public async Task<ViewDebt> GetDebtForViewByIdAsync(int debtId) =>
            _mapper.Map<ViewDebt>(await _debtRepository.GetDebtByIdAsync(debtId));

        [HttpPost]
        [Route("AddBorrowedDebt")]  
        public async Task<IActionResult> AddBorrowedDebtAsync(AddBorrowedDebt addBorrowedDebt)
        {
            await _debtRepository.CreateDebtAsync(_mapper.Map<Debt>(addBorrowedDebt));
            return Ok();
        }

        [HttpPost]
        [Route("AddLentDebt")]
        public async Task<IActionResult> AddLentDebtAsync(AddLentDebt addLentDebt)
        {
            await _debtRepository.CreateDebtAsync(_mapper.Map<Debt>(addLentDebt));
            return Ok();
        }

        [HttpDelete]
        [Route("{debtId}")]
        public async Task<IActionResult> DeleteDebtAsync(int debtId)
        {
            await _debtRepository.DeleteDebtAsync(await _debtRepository.GetDebtByIdAsync(debtId));
            return Ok();
        }

        private int GetCurrentUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}