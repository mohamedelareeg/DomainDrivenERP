using FluentValidation;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");

        RuleFor(command => command.Password)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(3).WithMessage("Password must be at least 3 characters long.")
            .Matches(@"^[^\s]+$").WithMessage("Password cannot contain spaces.")
            .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");
    }
}
