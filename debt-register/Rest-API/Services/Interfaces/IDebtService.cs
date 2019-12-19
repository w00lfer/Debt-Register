using System.Collections.Generic;
using System.Threading.Tasks;
using Rest_API.Models.DTOs;

namespace Rest_API.Services.Interfaces
{
    public interface IDebtService
    {
        Task<List<DebtForTable>> GetAllBorrowedDebtsAsync();
        Task<List<DebtForTable>> GetLastBorrowedDebtsAsync();
        Task<List<DebtToOrFromForTable>> GetAllBorrowedDebtsFromLenderAsync(int lenderId, bool isLocal);
        Task<List<DebtForTable>> GetAllLentDebtsAsync();
        Task<List<DebtForTable>> GetLastLentDebtsAsync();
        Task<List<DebtToOrFromForTable>> GetAllLentDebtsToBorrowerAsync(int borrowerId, bool isLocal);
        Task<ViewDebt> GetDebtForViewByIdAsync(int debtId);
        Task AddDebtAsync(AddBorrowedDebt addBorrowedDebt);
        Task AddDebtAsync(AddLentDebt addLentDebt);
        Task DeleteDebtAsync(int debtId);
    }
}
