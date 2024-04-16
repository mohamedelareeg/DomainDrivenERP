using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Errors;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.ValueObjects;
public sealed class SKU : ValueObject
{
    private const int MaxLength = 20;
    private const int MinLength = 5;
    private const string FormatPattern = @"^[A-Za-z0-9]{5,20}$";
    public string Value { get; }
    private SKU() { }
    private SKU(string value)
    {
        Value = value;
    }

    public static Result<SKU> Create(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
        {
            return Result.Failure<SKU>(DomainErrors.SkuErrors.InvalidSKU);
        }

        if (!Regex.IsMatch(sku, FormatPattern))
        {
            return Result.Failure<SKU>(DomainErrors.SkuErrors.InvalidSKUFormat);
        }

        if (sku.Length > MaxLength)
        {
            return Result.Failure<SKU>(DomainErrors.SkuErrors.InvalidSKULength);
        }

        if (sku.Length < MinLength)
        {
            return Result.Failure<SKU>(DomainErrors.SkuErrors.InvalidSKULength);
        }

        return Result.Success(new SKU(sku));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
