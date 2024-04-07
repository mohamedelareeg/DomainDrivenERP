﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
public interface IInvoiceRepository
{
    Task<CustomList<Invoice>> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber);
    Task<CustomList<Invoice>> GetAllCustomerInvoicesWithDapper(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber);
    Task<bool> IsInvoiceSerialExist(string invoiceSerial);
}