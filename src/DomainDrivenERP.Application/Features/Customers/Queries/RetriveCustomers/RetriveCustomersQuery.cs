﻿using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Customers;

public class RetriveCustomersQuery : IListQuery<Customer>
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
