using DomainDrivenERP.Application.Features.Authentication.Commands.Login;
using DomainDrivenERP.Application.Features.Authentication.Commands.Register;
using DomainDrivenERP.Application.Features.Authentication.Commands.ResetPassword;
using DomainDrivenERP.Application.Features.Authentication.Commands.SendResetPassword;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[AllowAnonymous]
[Route("api/v1/auth")]
public class AuthController : AppControllerBase
{
    public AuthController(ISender sender)
        : base(sender)
    {
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<LoginCommandResult> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<RegisterCommandResult> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("SendResetPasswordCode")]
    public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
}
