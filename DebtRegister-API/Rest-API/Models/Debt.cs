using System;
using System.ComponentModel.DataAnnotations;

namespace Rest_API.Models
{
    public class Debt : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [Range(0.0, double.MaxValue)]
        public decimal Value { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Debt starting date is required")]
        public DateTime DebtStartDate { get; set; }

        [Required(ErrorMessage = "Lender id is required")]
        public int LenderId { get; set; }

        [Required(ErrorMessage = "You must specify if lender is local or not")]
        public bool IsLenderLocal { get; set; }

        [Required(ErrorMessage = "Borrower id is required")]
        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "You must specify if lender is local or not")]
        public bool IsBorrowerLocal { get; set; }

        [Required(ErrorMessage = "You must specify if debt is payed or not")]
        public bool IsPayed { get; set; }
    }
}
