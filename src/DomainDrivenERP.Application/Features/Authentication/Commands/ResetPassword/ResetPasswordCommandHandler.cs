using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.ResetPassword;
internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, bool>
{
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _authService.ConfirmAndResetPassword(request.Code, request.Email, request.NewPassword);
    }
}
