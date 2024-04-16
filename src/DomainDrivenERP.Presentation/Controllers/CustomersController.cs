using DomainDrivenERP.Application.Features.Customers.Commands.CreateCustomer;
using DomainDrivenERP.Application.Features.Customers.Queries.GetCustomerInvoicesById;
using DomainDrivenERP.Application.Features.Customers.Queries.RetriveCustomer;
using DomainDrivenERP.Application.Features.Invoices.Commands.CreateCustomerInvoice;
using DomainDrivenERP.Application.Features.Invoices.Queries.RetriveCustomerInvoice;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;

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
        return CustomResult(result);
        // return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        Result<RetriveCustomerResponse> result = await Sender.Send(new RetriveCustomerQuery(id), cancellationToken);
        return CustomResult(result);
        // return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] RetriveCustomersQuery request, CancellationToken cancellationToken)
    {
        Result<CustomList<Customer>> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
        // return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    [HttpGet("info/invoices{id}")]
    public async Task<IActionResult> GetCustomerWithInvoices(string id, CancellationToken cancellationToken)
    {
        Result<Customer> result = await Sender.Send(new GetCustomerInvoicesByIdQuery(id), cancellationToken);
        return CustomResult(result);
    }
    [HttpGet("invoices")]
    public async Task<IActionResult> GetCustomerInvoices(
     string customerId,
     DateTime? startDate,
     DateTime? endDate,
     int pageSize = 10,
     int pageNumber = 1,
     CancellationToken cancellationToken = default)
    {
        Result<CustomList<Invoice>> result = await Sender.Send(new RetriveCustomerInvoicesQuery(customerId, startDate, endDate, pageSize, pageNumber), cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("invoices/create")]
    public async Task<IActionResult> CreateCustomerInvoice(CreateCustomerInvoiceCommand request , CancellationToken cancellationToken)
    {
        Result<Invoice> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

}
