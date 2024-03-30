using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Commands.DeleteCustomer;

internal class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, bool>
{
    public Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
