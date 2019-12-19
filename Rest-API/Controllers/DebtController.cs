using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rest_API.Models.DTOs;
using Rest_API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DebtController : ControllerBase
    {
        private readonly IDebtService _debtService;
        public DebtController(IDebtService debtService, IMapper mapper) =>  _debtService = debtService;

        [HttpGet]
        [Route("Borrowed")]
        public async Task<List<DebtForTable>> GetAllBorrowedDebtsAsync() =>
           await _debtService.GetAllBorrowedDebtsAsync();

        [HttpGet]
        [Route("LastBorrowed")]
        public async Task<List<DebtForTable>> GetLastBorrowedDebtsAsync() =>
            await _debtService.GetLastBorrowedDebtsAsync();
        
        [HttpGet]
        [Route("BorrowedFromLender/{lenderId}/{isLocal}")]
        public async Task<List<DebtToOrFromForTable>> GetAllBorrowedDebtsFromLenderAsync(int lenderId, bool isLocal) =>
           await _debtService.GetAllBorrowedDebtsFromLenderAsync(lenderId, isLocal);
 

        [HttpGet]
        [Route("Lent")]
        public async Task<List<DebtForTable>> GetAllLentDebtsAsync() =>
            await _debtService.GetAllLentDebtsAsync();

        [HttpGet]
        [Route("LastLent")]
        public async Task<List<DebtForTable>> GetLastLentDebtsAsync() =>
            await _debtService.GetLastLentDebtsAsync();

        [HttpGet]
        [Route("LentToBorrower/{borrowerId}/{isLocal}")]
        public async Task<List<DebtToOrFromForTable>> GetAllLentDebtsToBorrowerAsync(int borrowerId, bool isLocal) =>
           await _debtService.GetAllLentDebtsToBorrowerAsync(borrowerId, isLocal);


        [HttpGet]
        [Route("ViewDebt/{debtId}")]
        public async Task<ViewDebt> GetDebtForViewByIdAsync(int debtId) =>
            await _debtService.GetDebtForViewByIdAsync(debtId);

        [HttpPost]
        [Route("AddBorrowedDebt")]  
        public async Task<IActionResult> AddBorrowedDebtAsync(AddBorrowedDebt addBorrowedDebt)
        {
            await _debtService.AddDebtAsync(addBorrowedDebt);
            return Ok("You have added borrowed debt successfully");
        }

        [HttpPost]
        [Route("AddLentDebt")]
        public async Task<IActionResult> AddLentDebtAsync(AddLentDebt addLentDebt)
        {
            await _debtService.AddDebtAsync(addLentDebt);
            return Ok("You have added lent debt successfully");
        }

        [HttpDelete]
        [Route("{debtId}")]
        public async Task<IActionResult> DeleteDebtAsync(int debtId)
        {
            await _debtService.DeleteDebtAsync(debtId);
            return Ok("You have edited debt successfully");
        }
    }
}