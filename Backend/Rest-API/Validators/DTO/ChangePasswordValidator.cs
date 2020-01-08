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
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*.,]).*$").WithMessage("Password must contain at least one upper cased letter and one lowercased letter");
            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("Current password can't be empty")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*.,]).*$").WithMessage("Password must contain at least one upper cased letter and one lowercased letter");
        }
    }
}
