using AutoMapper;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Application.Features.Customers.Queries.RetriveCustomer;

public class RetriveCustomerMapping : Profile
{
    public RetriveCustomerMapping()
    {
        CreateMap<Customer, RetriveCustomerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));

        CreateMap<Dictionary<string, object>, Customer>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["Id"]))
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => FirstName.Create((string)src["FirstName"])))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => LastName.Create((string)src["LastName"])))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => Email.Create((string)src["Email"])))
              .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => (string)src["Phone"]));

        CreateMap<Dictionary<string, object>, RetriveCustomerResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["Id"]))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => FirstName.Create((string)src["FirstName"])))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => LastName.Create((string)src["LastName"])))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => Email.Create((string)src["Email"])))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => (string)src["Phone"]));
    }
}
