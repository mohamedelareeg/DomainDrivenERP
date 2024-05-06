using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Identity;

public interface IEmailsService
{
    Task<Result<bool>> SendEmail(string email, string message, string? reason, string companyName);
}
