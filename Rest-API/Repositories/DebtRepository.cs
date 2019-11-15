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

        public async Task<IEnumerable<Debt>> GetAllBorrowedDebtsAsync(int userId) =>
            await _appDbContext.Debts.Where(b => b.BorrowerId == userId).ToListAsync();

        public async Task<IEnumerable<Debt>> GetLastBorrowedDebtsAsync(int userId) =>
            await GetAllDebts().Where(b => b.BorrowerId == userId).OrderByDescending(d => d.DebtStartDate).Take(5).ToListAsync();

        // Gets borrowed debts from person depending on if its local contact or application user
        public async Task<IEnumerable<Debt>> GetAllBorrowedDebtsFromLenderAsync(int userId, bool isLocal, int lenderId) =>
            await GetAllDebts().Where(b => b.BorrowerId == userId && b.IsLenderLocal == isLocal && b.LenderId == lenderId).ToListAsync();

        public async Task<IEnumerable<Debt>> GetAllLentDebtsAsync(int userId) =>
            await GetAllDebts().Where(l => l.LenderId == userId).ToListAsync();

        public async Task<IEnumerable<Debt>> GetLastLentDebtsAsync(int userId) =>
            await GetAllDebts().Where(l => l.LenderId == userId).OrderByDescending(d => d.DebtStartDate).Take(5).ToListAsync();

        // Gets lent debts to person depending on if its local contact or application user
        public async Task<IEnumerable<Debt>> GetAllLentDebtsToBorrowerAsync(int userId, bool isLocal, int borrowerId) =>
             await GetAllDebts().Where(b => b.LenderId == userId && b.IsBorrowerLocal == isLocal && b.BorrowerId == borrowerId).ToListAsync();

        public async Task<Debt> GetDebtByIdAsync(int debtId) =>
            await _appDbContext.Debts.FirstOrDefaultAsync(x => x.Id == debtId);

        public async Task CreateDebtAsync(Debt debt)
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
