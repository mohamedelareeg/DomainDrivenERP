using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Application.Features.Authentication.Commands.Login;
public class LoginCommandResult
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
