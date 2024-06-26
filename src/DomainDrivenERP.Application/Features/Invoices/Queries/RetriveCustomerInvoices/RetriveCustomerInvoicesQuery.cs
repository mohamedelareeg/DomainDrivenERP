﻿using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Invoices;
using System;

namespace DomainDrivenERP.Application.Features.Invoices.Queries.RetriveCustomerInvoice;

public class RetriveCustomerInvoicesQuery : IListQuery<Invoice>
{
    public RetriveCustomerInvoicesQuery(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber)
    {
        CustomerId = customerId;
        StartDate = startDate;
        EndDate = endDate;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public string CustomerId { get; }
    public DateTime? StartDate { get; }
    public DateTime? EndDate { get; }
    public int PageSize { get; }
    public int PageNumber { get; }
}
