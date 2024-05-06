
using DomainDrivenERP.Application.Features.Authentication.Commands.Login;
using DomainDrivenERP.Application.Features.Authentication.Commands.Register;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Identity;

public interface IAuthService
{
    Task<Result<LoginCommandResult>> Login(string email, string password);
    Task<Result<RegisterCommandResult>> Register(string firstName, string lastName, string email, string userName, string password);
    Task<Result<bool>> SendResetPasswordCode(string email);
    Task<Result<bool>> ConfirmAndResetPassword(string code, string email, string newPassword);
}
