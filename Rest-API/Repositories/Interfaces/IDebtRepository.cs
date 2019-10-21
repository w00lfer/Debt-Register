using Rest_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Repositories.Interfaces
{
    public interface IDebtRepository
    {
        Task<IQueryable<Debt>> GetAllLentDebts(int lenderId);
        Task<IQueryable<Debt>> GetAllBorrowedDebts(int borrowerId);
        Task<Debt> GetDebtById(int debtId);
        Task AddDebt(Debt debt);
        Task EditDebt(Debt debt);
        Task DeleteDebt(Debt debt);    
        
    }
}
