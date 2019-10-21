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

        public async Task<IQueryable<Debt>> GetAllBorrowedDebts(int borrowerId) // => await _appDbContext.Debts.Where(x => x.BorrowerId == borrowerId).ToListAsync();
        {
            throw new System.NotImplementedException();
        }
        public async Task<IQueryable<Debt>> GetAllLentDebts(int lenderId)
        {
            throw new System.NotImplementedException();
        }
        public async Task<Debt> GetDebtById(int debtId) => await _appDbContext.Debts.FirstOrDefaultAsync(x => x.DebtId == debtId);
        public async Task AddDebt(Debt debt)
        {
            await _appDbContext.Debts.AddAsync(debt);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteDebt(Debt debt)
        {
            _appDbContext.Remove(debt);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task EditDebt(Debt debt)
        {
            _appDbContext.Update(debt);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
