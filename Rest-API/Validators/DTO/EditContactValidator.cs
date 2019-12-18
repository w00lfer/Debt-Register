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
                .Matches(@"^([A-Za-z]{3,})+\s+([A-Za-z]{3,})+$").WithMessage("Fullname must contain fullname and surrname, each one with atleast 3characters");
            RuleFor(c => c.PhoneNumber)
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");
        }
    }
}

