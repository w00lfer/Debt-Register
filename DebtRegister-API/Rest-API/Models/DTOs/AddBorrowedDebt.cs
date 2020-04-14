using System;

namespace Rest_API.Models.DTOs
{
    public class AddBorrowedDebt
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime DebtStartDate { get; set; }
        public int LenderId { get; set; }
        public bool IsLenderLocal { get; set; }
        public bool IsPayed { get; set; }
    }
}
