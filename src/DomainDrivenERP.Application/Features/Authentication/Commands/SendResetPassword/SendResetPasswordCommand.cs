using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.SendResetPassword;
public class SendResetPasswordCommand : ICommand<bool>
{
    public string Email { get; set; }
}
