using System;
using System.ComponentModel.DataAnnotations;

namespace Rest_API.Models
{
    public class Debt
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime DebtStartDate { get; set; }
        public DateTime? DebtPaymentDate { get; set; }
        public int? LenderId { get; set; }
        public string? LenderFullName { get; set; }
        public int? BorrowerId { get; set; }
        public string? BorrowerFullName { get; set; }
        public bool IsPayed { get; set; }
    }
}
