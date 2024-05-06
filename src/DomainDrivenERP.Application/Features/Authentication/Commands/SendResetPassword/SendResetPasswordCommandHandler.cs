using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.SendResetPassword;
internal class SendResetPasswordCommandHandler : ICommandHandler<SendResetPasswordCommand, bool>
{
    private readonly IAuthService _authService;

    public SendResetPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<Result<bool>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _authService.SendResetPasswordCode(request.Email);
    }
}
