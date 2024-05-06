using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.Login;
public class LoginCommand : ICommand<LoginCommandResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
