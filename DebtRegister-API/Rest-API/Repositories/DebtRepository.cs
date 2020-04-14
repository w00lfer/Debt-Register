using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rest_API.Models;
using Rest_API.Repositories.Interfaces;

namespace Rest_API.Repositories
{
    public class DebtRepository : GenericRepository<Debt>, IDebtRepository
    {
        public DebtRepository(AppDbContext appDbContext)
            : base(appDbContext)
        { }
        public async Task<IEnumerable<Debt>> GetAllBorrowedDebtsAsync(int userId) =>
            await GetAll().Where(b => b.BorrowerId == userId && b.IsBorrowerLocal == false).ToListAsync();

        public async Task<IEnumerable<Debt>> GetLastBorrowedDebtsAsync(int userId) =>
            await GetAll().Where(b => b.BorrowerId == userId && b.IsBorrowerLocal == false).OrderByDescending(d => d.DebtStartDate).Take(5).ToListAsync();

        // Gets borrowed debts from person depending on if its local contact or application user
        public async Task<IEnumerable<Debt>> GetAllBorrowedDebtsFromLenderAsync(int userId, bool isLocal, int lenderId) =>
            await GetAll().Where(b => b.BorrowerId == userId && b.IsLenderLocal == isLocal && b.LenderId == lenderId).ToListAsync();

        public async Task<IEnumerable<Debt>> GetAllLentDebtsAsync(int userId) =>
            await GetAll().Where(l => l.LenderId == userId && l.IsLenderLocal == false).ToListAsync();

        public async Task<IEnumerable<Debt>> GetLastLentDebtsAsync(int userId) =>
            await GetAll().Where(l => l.LenderId == userId && l.IsLenderLocal == false).OrderByDescending(d => d.DebtStartDate).Take(5).ToListAsync();

        // Gets lent debts to person depending on if its local contact or application user
        public async Task<IEnumerable<Debt>> GetAllLentDebtsToBorrowerAsync(int userId, bool isLocal, int borrowerId) =>
             await GetAll().Where(b => b.LenderId == userId && b.IsBorrowerLocal == isLocal && b.BorrowerId == borrowerId).ToListAsync();
    }
}
