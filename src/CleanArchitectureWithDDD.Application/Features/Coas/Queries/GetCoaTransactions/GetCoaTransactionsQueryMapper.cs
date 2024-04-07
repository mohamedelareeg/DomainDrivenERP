using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Transactions;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaTransactions;
internal class GetCoaTransactionsQueryMapper : Profile
{
    public GetCoaTransactionsQueryMapper()
    {
        CreateMap<Transaction, JournalTransactionsDto>();

    }
}
