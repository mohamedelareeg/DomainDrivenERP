using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Errors;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.ValueObjects;
public sealed class Price : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }
    private Price() { }
    private Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Price> Create(decimal amount, string currency)
    {
        if (amount <= 0)
        {
            return Result.Failure<Price>(DomainErrors.PriceErrors.InvalidAmount);
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            return Result.Failure<Price>(DomainErrors.PriceErrors.InvalidCurrency);
        }
        return Result.Success(new Price(amount, currency));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}
