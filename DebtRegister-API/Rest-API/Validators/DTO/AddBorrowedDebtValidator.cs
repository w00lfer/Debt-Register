using FluentValidation;
using Rest_API.Models.DTOs;

namespace Rest_API.Validators.DTO
{
    public class AddBorrowedDebtValidator : AbstractValidator<AddBorrowedDebt>
    {
        public AddBorrowedDebtValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Debt name can't be empty");
            RuleFor(d => d.Value)
                .NotEmpty().WithMessage("Value can't be empty")
                .GreaterThan(0).WithMessage("Value must be higher than 1");
            RuleFor(d => d.LenderId)
                .GreaterThanOrEqualTo(1).WithMessage("Lender id must be higher than 1");    
        }
    }
}
