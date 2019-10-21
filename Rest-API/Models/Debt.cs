using System;

namespace Rest_API.Models
{
    public class Debt
    {
        public int DebtId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime DebtStartDate { get; set; }
        public DateTime? DebtPaymentDate { get; set; }
        public int? LenderId { get; set; }
        public string? LenderFullName { get; set; }
        public int? BorrowerId { get; set; }
        public string? BorrowerFullName { get; set; }
        public bool IsImportant { get; set; }
    }
}
