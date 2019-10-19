using System.Collections;
using System.Collections.Generic;
using Rest_API.Models;

namespace Rest_API.Repositories.Interfaces
{
    public interface IDebtRepository
    {
        IEnumerable<Debt> GetAllLentDebts(int lenderId, int? borrowerId);
        IEnumerable<Debt> GetAllBorrowedDebts(int borrowerId, int? lenderId);
    }
}
