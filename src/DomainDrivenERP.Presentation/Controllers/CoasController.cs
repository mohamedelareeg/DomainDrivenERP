using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Features.Coas.Commands.CreateCoa;
using DomainDrivenERP.Application.Features.Coas.Commands.CreateFirstLevelCoa;
using DomainDrivenERP.Application.Features.Coas.Queries.GetCoaTransactions;
using DomainDrivenERP.Application.Features.Coas.Queries.GetCoaWithChildrens;
using DomainDrivenERP.Application.Features.Journals.Queries.GetJournalById;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.COAs;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[Microsoft.AspNetCore.Mvc.Route("api/v1/coas")]
public sealed class CoasController : AppControllerBase
{
    public CoasController(ISender sender) : base(sender)
    {
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCoa(CreateCoaCommand request, CancellationToken cancellationToken)
    {
        Result<COA> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
    [HttpPost("create/firstLevel")]
    public async Task<IActionResult> CreateFirstCoaLevel(CreateFirstLevelCoaCommand request, CancellationToken cancellationToken)
    {
        Result<COA> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
    [HttpGet("coa-tree/{id}")]
    public async Task<IActionResult> GetCoaWithChildrens(string id, CancellationToken cancellationToken)
    {
        Result<CoaWithChildrenDto> result = await Sender.Send(new GetCoaWithChildrensQuery(id), cancellationToken);
        return CustomResult(result);
    }
    [HttpGet("transactions")]
    public async Task<IActionResult> GetJournalCoaTransactions(
     string? accountName,
     string? accountHeadCode,
     DateTime? startDate,
     DateTime? endDate,
     CancellationToken cancellationToken)
    {
        var query = new GetCoaTransactionsQuery(
            accountName,
            accountHeadCode,
            startDate,
            endDate);

        Result<CustomList<JournalTransactionsDto>> result = await Sender.Send(query, cancellationToken);
        return CustomResult(result);
    }


}
