using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Commands;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Validators.Commands
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(Email.MaxLength).WithMessage($"Email must not exceed {Email.MaxLength} characters.")
                .EmailAddress().WithMessage("Email is not in a valid format.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FirstName.MaxLength).WithMessage($"First name must not exceed {FirstName.MaxLength} characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(LastName.MaxLength).WithMessage($"Last name must not exceed {LastName.MaxLength} characters.");
        }
    }
}
