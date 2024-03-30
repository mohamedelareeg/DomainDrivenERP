namespace CleanArchitectureWithDDD.Persistence.Constants;

internal static class TableNames
{
    internal const string Customers = nameof(Customers);
    internal const string Invoices = nameof(Invoices);
    internal const string OutboxMessages = nameof(OutboxMessages);
    internal const string OutboxMessageConsumers = nameof(OutboxMessageConsumers);
}
