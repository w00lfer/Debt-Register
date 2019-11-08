using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;

namespace Rest_API.Repositories
{
    public class DebtRepository : IDebtRepository
    {
        private AppDbContext _appDbContext;
        public DebtRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public IQueryable<Debt> GetAllDebts() => _appDbContext.Debts.AsQueryable();

        public async Task<IEnumerable<Debt>> GetAllBorrowedDebtsAsync(int borrowerId) =>
            await _appDbContext.Debts.Where(b => b.BorrowerId == borrowerId).ToListAsync();

        public async Task<IEnumerable<Debt>> GetLastBorrowedDebtsAsync(int borrowerId) =>
            await GetAllDebts().Where(b => b.BorrowerId == borrowerId)
                .OrderByDescending(d => d.DebtStartDate).Take(5).ToListAsync();

        public async Task<IEnumerable<Debt>> GetAllLentDebtsAsync(int lenderId) =>
            await GetAllDebts().Where(l => l.LenderId == lenderId).ToListAsync();

        public async Task<IEnumerable<Debt>> GetLastLentDebtsAsync(int lenderId) =>
            await GetAllDebts().Where(l => l.LenderId == lenderId)
                .OrderByDescending(d => d.DebtStartDate).Take(5).ToListAsync();
        public async Task<Debt> GetDebtByIdAsync(int debtId) =>
            await _appDbContext.Debts.FirstOrDefaultAsync(x => x.Id == debtId);

        public async Task AddDebtAsync(Debt debt)
        {
            await _appDbContext.Debts.AddAsync(debt);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteDebtAsync(Debt debt)
        {
            _appDbContext.Remove(debt);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task EditDebtAsync(Debt debt)
        {
            _appDbContext.Update(debt);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
