using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.Register;
public class RegisterCommand : ICommand<RegisterCommandResult>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
