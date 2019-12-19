using FluentValidation;
using Rest_API.Models.DTOs;

namespace Rest_API.Validators.DTO
{
    public class AddContactValidator : AbstractValidator<AddContact>
    {
        public AddContactValidator()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("Fullname can't be empty")
                .Matches(@"^([A-Za-z]{3,})+\s+([A-Za-z]{3,})+$").WithMessage("Fullname must contain fullname and surrname, each one with atleast 3 characters");
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can't be empty")
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");
        }
    }
}
