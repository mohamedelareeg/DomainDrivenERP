using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Emails.Commands.SendEmail;
public class SendEmailCommand : ICommand<bool>
{
    public string Email { get; set; }
    public string Message { get; set; }
}
