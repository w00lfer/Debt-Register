using Rest_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IDebtRepository
    {
        IQueryable<Debt> GetAllDebts();
        Task<IEnumerable<Debt>> GetAllBorrowedDebtsAsync(int userId);
        Task<IEnumerable<Debt>> GetLastBorrowedDebtsAsync(int userId);
        Task<IEnumerable<Debt>> GetAllBorrowedDebtsFromLenderAsync(int userId, bool isLocal, int lenderId);
        Task<IEnumerable<Debt>> GetAllLentDebtsAsync(int userId);
        Task<IEnumerable<Debt>> GetLastLentDebtsAsync(int userId);
        Task<IEnumerable<Debt>> GetAllLentDebtsToBorrowerAsync(int userId, bool isLocal, int borrowerId);
        Task<Debt> GetDebtByIdAsync(int debtId);
        Task CreateDebtAsync(Debt debt);
        Task EditDebtAsync(Debt debt);
        Task DeleteDebtAsync(Debt debt);    
        
    }
}
