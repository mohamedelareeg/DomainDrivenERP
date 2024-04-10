using CleanArchitectureWithDDD.Domain.Shared.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers.Specifications;

public static class GetCustomerInvoicesByIdSpecification
{
    public static BaseSpecification<Customer> GetCustomerInvoicesByIdSpec(string customerId)
    {
        var spec = new BaseSpecification<Customer>(
            c => c.Id.ToString() == customerId
        );
        spec.AddInclude(customer => customer.Invoices);
        return spec;
    }
}
