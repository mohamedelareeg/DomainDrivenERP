using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;

public class RetriveCustomersQuery : IRequest<Result<List<Customer>>>
{
    public int? Page { get; }
    public int? PageSize { get; }

    public RetriveCustomersQuery() { }

    public RetriveCustomersQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }

}
