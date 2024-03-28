using CleanArchitectureWithDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Abstractions
{
    public interface ICustomerRespository
    {
        Task<Customer> GetByIdAsync(Guid CustomerId);
        Task UpdateAsync(Customer customer);
    }
}
