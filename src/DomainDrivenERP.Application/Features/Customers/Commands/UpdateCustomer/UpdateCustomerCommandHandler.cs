using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Customers.Commands.UpdateCustomer;

internal class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, bool>
{
    public Task<Result<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
