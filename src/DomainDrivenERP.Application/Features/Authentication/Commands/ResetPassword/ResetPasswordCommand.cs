using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.ResetPassword;
public class ResetPasswordCommand : ICommand<bool>
{
    public string Code { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
}
