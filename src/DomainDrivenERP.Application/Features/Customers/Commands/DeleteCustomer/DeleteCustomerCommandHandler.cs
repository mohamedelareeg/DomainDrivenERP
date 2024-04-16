using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Customers.Commands.DeleteCustomer;

internal class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, bool>
{
    public Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
