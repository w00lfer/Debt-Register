using Microsoft.AspNetCore.Mvc;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;
using System;
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
        public async Task<Object> GetAllBorrowedDebtsForUserAsync(int userId) => await _debtRepository.GetAllBorrowedDebtsAsync(userId);

        [HttpGet]
        [Route("{userId}/LastBorrowed")]
        public async Task<Object> GetLastBorrowedDebtsForUserAsync(int userId) =>
            await _debtRepository.GetLastBorrowedDebtsAsync(userId);

        [HttpGet]
        [Route("{userId}/Lent")]
        public async Task<Object> GetAllLentDebtsForUserAsync(int userId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{userId}/LastLent")]
        public async Task<Object> GetLastLentDebtsForUserAsync(int userId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{debtId}")]
        public async Task<Object> GetDebtForUserAsync(int debtId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("AddDebt")]
        public async Task<Object> AddDebt(Debt debt)
        {
            await _debtRepository.AddDebtAsync((debt));
            return Ok();
        }
    }
}