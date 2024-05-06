using System.Reflection;
using DomainDrivenERP.Application.Security;
using DomainDrivenERP.Domain.Abstractions.Identity;
using MediatR;

namespace DomainDrivenERP.Application.Behaviors;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(
        IUser user,
        IIdentityService identityService)
    {
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        IEnumerable<AuthorizeAttribute> authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == null)
            {
                throw new UnauthorizedAccessException();
            }
            IEnumerable<AuthorizeAttribute> authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                bool authorized = false;

                foreach (string[]? roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (string role in roles)
                    {
                        Domain.Shared.Results.Result<bool> isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
                        if (isInRole.Value)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            // Claims-based authorization
            IEnumerable<AuthorizeAttribute> authorizeAttributesWithClaims = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Claims));
            if (authorizeAttributesWithClaims.Any())
            {
                bool authorized = false;

                foreach (string[]? claimTypeValuePairs in authorizeAttributesWithClaims.Select(a => a.Claims.Split(',')))
                {
                    foreach (string claimTypeValuePair in claimTypeValuePairs)
                    {
                        string[] parts = claimTypeValuePair.Trim().Split(':');
                        if (parts.Length == 2)
                        {
                            string claimType = parts[0];
                            string claimValue = parts[1];

                            Domain.Shared.Results.Result<bool> hasClaim = await _identityService.HasClaim(_user.Id, claimType, claimValue);
                            if (hasClaim.Value)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }
                }

                // Must have at least one required claim
                if (!authorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();

    }
}
