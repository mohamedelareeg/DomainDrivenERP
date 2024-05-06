using DomainDrivenERP.Application.Features.Roles.Commands.AddClaimToRole;
using DomainDrivenERP.Application.Features.Roles.Commands.AssignClaimToUser;
using DomainDrivenERP.Application.Features.Roles.Commands.AssignRoleToUser;
using DomainDrivenERP.Application.Features.Roles.Commands.CreateRole;
using DomainDrivenERP.Application.Features.Roles.Commands.DeleteRole;
using DomainDrivenERP.Application.Features.Roles.Commands.EditRole;
using DomainDrivenERP.Application.Features.Roles.Queries.GetAllClaims;
using DomainDrivenERP.Application.Features.Roles.Queries.GetAllRoles;
using DomainDrivenERP.Application.Features.Roles.Queries.GetRoleClaims;
using DomainDrivenERP.Application.Features.Roles.Queries.GetUserClaims;
using DomainDrivenERP.Application.Features.Roles.Queries.GetUserRoles;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[Route("api/v1/roles")]
public class RolesController : AppControllerBase
{
    public RolesController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("EditRole")]
    public async Task<IActionResult> EditRole([FromBody] EditRoleCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("DeleteRole")]
    public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("GetRoleClaims")]
    public async Task<IActionResult> GetRoleClaims(string roleName, CancellationToken cancellationToken)
    {
        var query = new GetRoleClaimsQuery { RoleName = roleName };
        Domain.Shared.Results.Result<Domain.Shared.Results.CustomList<string>> result = await Sender.Send(query, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("AddClaimToRole")]
    public async Task<IActionResult> AddClaimToRole([FromBody] AddClaimToRoleCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("AssignClaimToUser")]
    public async Task<IActionResult> AssignClaimToUser([FromBody] AssignClaimToUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Shared.Results.Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserRolesQuery { UserId = userId };
        Domain.Shared.Results.Result<Domain.Shared.Results.CustomList<string>> result = await Sender.Send(query, cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("GetUserClaims")]
    public async Task<IActionResult> GetUserClaims(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserClaimsQuery { UserId = userId };
        Domain.Shared.Results.Result<Domain.Shared.Results.CustomList<string>> result = await Sender.Send(query, cancellationToken);
        return CustomResult(result);
    }
    [Authorize(Roles = "Administrator")]
    [HttpGet("GetAllRoles")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var query = new GetAllRolesQuery();
        Domain.Shared.Results.Result<Domain.Shared.Results.CustomList<string>> result = await Sender.Send(query, cancellationToken);
        return CustomResult(result);
    }
    [HttpGet("GetAllClaims")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAllClaims(CancellationToken cancellationToken)
    {
        var query = new GetAllClaimsQuery();
        Domain.Shared.Results.Result<Domain.Shared.Results.CustomList<string>> result = await Sender.Send(query, cancellationToken);
        return CustomResult(result);
    }
}
