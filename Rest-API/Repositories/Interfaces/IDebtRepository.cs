using Rest_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IDebtRepository
    {
        IQueryable<Debt> GetAllDebts();
        Task<IEnumerable<Debt>> GetAllBorrowedDebtsAsync(int borrowerId);
        Task<IEnumerable<Debt>> GetLastBorrowedDebtsAsync(int borrowerId);
        Task<IEnumerable<Debt>> GetAllLentDebtsAsync(int lenderId);
        Task<IEnumerable<Debt>> GetLastLentDebtsAsync(int lenderId);
        Task<Debt> GetDebtByIdAsync(int debtId);
        Task AddDebtAsync(Debt debt);
        Task EditDebtAsync(Debt debt);
        Task DeleteDebtAsync(Debt debt);    
        
    }
}
