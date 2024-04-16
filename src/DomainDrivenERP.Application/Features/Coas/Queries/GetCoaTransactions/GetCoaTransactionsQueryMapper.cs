using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.Transactions;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaTransactions;
internal class GetCoaTransactionsQueryMapper : Profile
{
    public GetCoaTransactionsQueryMapper()
    {
        CreateMap<Transaction, JournalTransactionsDto>();

    }
}
