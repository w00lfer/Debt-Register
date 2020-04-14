using System;

namespace Rest_API.Models.DTOs
{
    public class ViewDebt
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime DebtStartDate { get; set; }
        public string ContactFullName { get; set; }
        public bool IsPayed { get; set; }
    }
}
