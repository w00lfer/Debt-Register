using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtController : ControllerBase
    {
        private readonly IDebtRepository _debtRepository;
        public DebtController(IDebtRepository debtRepository) => _debtRepository = debtRepository;

        [HttpGet]
        [Route("{userId}/Borrowed")]
        public async Task<Object> GetAllBorrowedDebtsForUserAsync(int userId) =>
            await _debtRepository.GetAllBorrowedDebtsAsync(userId);

        [HttpGet]
        [Route("{userId}/LastBorrowed")]
        public async Task<Object> GetLastBorrowedDebtsForUserAsync(int userId) =>
            await _debtRepository.GetLastBorrowedDebtsAsync(userId);

        [HttpGet]
        [Route("{userId}/Lent")]
        public async Task<Object> GetAllLentDebtsForUserAsync(int userId) =>
            await _debtRepository.GetAllLentDebtsAsync(userId);

        [HttpGet]
        [Route("{userId}/LastLent")]
        public async Task<Object> GetLastLentDebtsForUserAsync(int userId) =>
            await _debtRepository.GetLastLentDebtsAsync(userId);

        [HttpGet]
        [Route("{debtId}")]
        public async Task<Object> GetDebtForUserAsync(int debtId) =>
            await _debtRepository.GetDebtByIdAsync(debtId);

        [HttpPost]
        [Route("AddDebt")]
        public async Task AddDebtAsync(Debt debt) => await _debtRepository.AddDebtAsync((debt));

        [HttpPut]
        [Route("{debtId}")]
        public async Task EditDebtAsync(Debt debt) => await _debtRepository.EditDebtAsync(debt);

        [HttpDelete]
        [Route("{debtId}")]
        public async Task DeleteDebtAsync(Debt debt) => await _debtRepository.AddDebtAsync(debt);
    }
}