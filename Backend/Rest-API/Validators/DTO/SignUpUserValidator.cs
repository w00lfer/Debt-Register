using FluentValidation;
using Rest_API.Models.DTOs;

namespace Rest_API.Validators.DTO
{
    public class SignUpValidator : AbstractValidator<SignUpUser>
    {
        public SignUpValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Username can't be empty")
                .Length(6, 16).WithMessage("Username's length must be between 6 and 16 characters ");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .Length(6, 16).WithMessage("Password's length must be between 6 and 16 characters")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*.,]).*$").WithMessage("Password must contain at least one upper cased letter and one lowercased letter");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .EmailAddress().WithMessage("Email address is invalid");
            RuleFor(u => u.TelephoneNumber)
                .NotEmpty().WithMessage("Phone number can't be empty")
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage("Fullname is required")
                .Matches(@"^([A-Za-z]{3,})+\s+([A-Za-z]{3,})+$").WithMessage("Fullname must contain fullname and surname, each one with at least 3 characters");
        }
    }
}
