using FluentValidation;
using Rest_API.Models.DTOs;

namespace Rest_API.Validators.DTO
{
    public class ChangePasswordValidator : AbstractValidator<ChangePassword>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty().WithMessage("Current password can't be empty")
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");
            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("Current password can't be empty")
                .Matches(@"^([0-9]{3})([0-9]{3})([0-9]{3})$").WithMessage("Telephone number is invalid");
        }
    }
}
