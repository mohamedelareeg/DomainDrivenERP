using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Features.Journals.Commands.CreateJournal;
using DomainDrivenERP.Application.Features.Journals.Queries.GetJournalById;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[Microsoft.AspNetCore.Mvc.Route("api/v1/journals")]
public class JournalsController : AppControllerBase
{
    public JournalsController(ISender sender) : base(sender)
    {
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateJorunal(CreateJournalCommand request, CancellationToken cancellationToken)
    {
        Result<Journal> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJournalById(string id, CancellationToken cancellationToken)
    {
        Result<JournalDto> result = await Sender.Send(new GetJournalByIdQuery(id), cancellationToken);
        return CustomResult(result);
    }

}
