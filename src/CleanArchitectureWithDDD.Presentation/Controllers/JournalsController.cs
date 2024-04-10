using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Features.Journals.Commands.CreateJournal;
using CleanArchitectureWithDDD.Application.Features.Journals.Queries.GetJournalById;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWithDDD.Presentation.Controllers;
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
