using FluentValidation;
using Rest_API.Models.DTOs;

namespace Rest_API.Validators.DTO
{
    public class EditContactValidator : AbstractValidator<EditContact>
    {
        public EditContactValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThanOrEqualTo(1).WithMessage("Contact id must be greater than 1");
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("Fullname can't be empty")
                .Matches(@"^([A-Za-z]{3,})+\s+([A-Za-z]{3,})+$").WithMessage("Fullname must contain fullname and surname, each one with at least 3characters");
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can't be empty")
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");
        }
    }
}

