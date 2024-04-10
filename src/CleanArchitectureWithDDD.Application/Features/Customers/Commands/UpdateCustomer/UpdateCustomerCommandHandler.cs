using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Commands.UpdateCustomer;

internal class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, bool>
{
    public Task<Result<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
