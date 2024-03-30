using CleanArchitectureWithDDD.Application.Features.Customers.Commands.CreateCustomer;
using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Queries;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Presentation.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/v1/customers")]
    public sealed class CustomersController : AppControllerBase
    {
        public CustomersController(ISender sender):base(sender)
        {

        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request, cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerById(Guid id , CancellationToken cancellationToken)
        {
            var result = await Sender.Send(new RetriveCustomerQuery(id), cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery]RetriveCustomersQuery request , CancellationToken cancellationToken)
        {
            var result = await Sender.Send(request,cancellationToken);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Ok(result.Value);
        }

    }
}
