using FluentValidation;
using Rest_API.Models.DTOs;

namespace Rest_API.Validators.DTO
{
    public class SignUpValidator : AbstractValidator<SignUpUser>
    {
        public SignUpValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Username can't be empy")
                .Length(6, 16).WithMessage("Username's length must be between 6 and 16 characters ");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .Length(6, 16).WithMessage("Password's length must be between 6 and 16 characters")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*.,]).*$").WithMessage("Password must contain atleast one uppercased letter and one lowercased letter");
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Email adress is invalid");
            RuleFor(u => u.TelephoneNumber)
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");

        }
    }
}
