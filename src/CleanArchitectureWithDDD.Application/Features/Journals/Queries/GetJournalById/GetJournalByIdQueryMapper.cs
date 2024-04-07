using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Entities.Transactions;

namespace CleanArchitectureWithDDD.Application.Features.Journals.Queries.GetJournalById;
internal class GetJournalByIdQueryMapper : Profile
{
    public GetJournalByIdQueryMapper()
    {
        CreateMap<Journal, JournalDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.IsOpening, opt => opt.MapFrom(src => src.IsOpening))
               .ForMember(dest => dest.JournalDate, opt => opt.MapFrom(src => src.JournalDate))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
               .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));

        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.Debit))
            .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.Credit))
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.COA.HeadName))
            .ForMember(dest => dest.AccountHeadCode, opt => opt.MapFrom(src => src.COAId));
    }
}
