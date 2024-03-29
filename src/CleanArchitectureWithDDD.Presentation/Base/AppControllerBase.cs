using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Presentation.Base
{
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        protected readonly ISender Sender;

        protected AppControllerBase(ISender sender)
        {
            Sender = sender;
        }
    }
}
