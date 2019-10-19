using System;

namespace Rest_API.Models
{
    public class Debt
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime DebtStartDate { get; set; }
        public DateTime? DebtPaymentDate { get; set; }
        public int? LenderId { get; set; }
        public string? LenderName { get; set; }
        public int? BorrowerId { get; set; }
        public string? BorrowerName { get; set; }
        public bool IsImportant { get; set; }
    }
}
