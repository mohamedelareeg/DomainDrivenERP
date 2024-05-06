namespace DomainDrivenERP.Domain.Abstractions.Identity;

public interface IUser
{
    string? Id { get; }
    string? Email { get; }
}
