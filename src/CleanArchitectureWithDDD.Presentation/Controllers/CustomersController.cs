using CleanArchitectureWithDDD.Application.Features.Customers.Commands.CreateCustomer;
using CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWithDDD.Presentation.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/v1/customers")]
public sealed class CustomersController : AppControllerBase
{
    public CustomersController(ISender sender) : base(sender)
    {

    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Result<Customer> result = await Sender.Send(request, cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        Result<RetriveCustomerResponse> result = await Sender.Send(new RetriveCustomerQuery(id), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] RetriveCustomersQuery request, CancellationToken cancellationToken)
    {
        Result<List<Customer>> result = await Sender.Send(request, cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

}
