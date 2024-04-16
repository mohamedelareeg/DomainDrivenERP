using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Domain.Entities.Customers;
public sealed class CustomerSnapshot
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public DateTime CreatedOnUtc { get; init; }
    public DateTime? ModifiedOnUtc { get; init; }
}
