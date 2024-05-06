using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.Login;
internal class LoginCommandHandler : ICommandHandler<LoginCommand, LoginCommandResult>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<LoginCommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.Email, request.Password);
    }
}
