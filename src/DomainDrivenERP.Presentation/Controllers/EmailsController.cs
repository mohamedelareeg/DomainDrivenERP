using DomainDrivenERP.Application.Features.Emails.Commands.SendEmail;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[Authorize(Roles = "Administrator")]
[Route("api/v1/email")]
public class EmailsController : AppControllerBase
{
    public EmailsController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
}
