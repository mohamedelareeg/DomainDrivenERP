using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Identity;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.Register;
internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterCommandResult>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<RegisterCommandResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Register(request.FirstName, request.LastName, request.Email, request.UserName, request.Password);
    }
}
