using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DomainDrivenERP.Domain.Entities.COAs.DomainEvents;
using DomainDrivenERP.Domain.Entities.Transactions;
using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Entities.COAs;

public sealed class COA : AggregateRoot
{
    private readonly List<COA> _coas = new();
    private readonly List<Transaction> _transactions = new();
    public COA()
    {

    }
    public COA(string headCode, string headName, string parentHeadCode, bool isGl, COA_Type type, int headLevel)
    {
        Guard.Against.NullOrEmpty(headCode, nameof(headCode));
        Guard.Against.NullOrEmpty(headName, nameof(headName));
        Guard.Against.NullOrEmpty(parentHeadCode, nameof(parentHeadCode));

        HeadCode = headCode;
        HeadName = headName;
        ParentHeadCode = parentHeadCode;
        IsGl = isGl;
        Type = type;
        HeadLevel = headLevel;
    }
    public COA(string headCode, string headName, bool isGl, COA_Type type)
    {
        Guard.Against.NullOrEmpty(headCode, nameof(headCode));
        Guard.Against.NullOrEmpty(headName, nameof(headName));
        HeadCode = headCode;
        HeadName = headName;
        IsGl = isGl;
        Type = type;
        HeadLevel = 1;
    }
    public string HeadCode { get; private set; }
    public string HeadName { get; private set; }
    public string ParentHeadCode { get; private set; }
    public int HeadLevel { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsGl { get; private set; }
    public COA_Type Type { get; private set; }
    public IReadOnlyCollection<COA> COAs => _coas;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    public COA ParentCOA { get; private set; }

    public static Result<COA> Create(string headName, COA? parentCoa, bool isGl = false)
    {
        Guard.Against.NullOrEmpty(headName, nameof(headName));
        Guard.Against.Null(parentCoa, nameof(parentCoa));

        if (parentCoa == null)
        {
            return Result.Failure<COA>(new Error("COA.Create", "Parent COA cannot be null."));
        }

        try
        {
            int headLevel = parentCoa.HeadLevel + 1;
            string headCode = GenerateNextHeadCode(parentCoa);

            var coa = new COA(headCode, headName, parentCoa.HeadCode, isGl, parentCoa.Type, headLevel);
            parentCoa._coas.Add(coa);
            coa.RaiseDomainEvent(new CreateCOADomainEvent(headName, parentCoa.HeadCode, parentCoa.Type));

            return coa;
        }
        catch (Exception ex)
        {
            return Result.Failure<COA>(new Error("COA.Create", $"Failed to create COA: {ex.Message}"));
        }
    }
    public static Result<COA> Create(string headName, string headCode, COA_Type type, bool isGl = false)
    {
        Guard.Against.NullOrEmpty(headName, nameof(headName));
        Guard.Against.NullOrEmpty(headCode, nameof(headCode));

        if (string.IsNullOrEmpty(headCode))
        {
            return Result.Failure<COA>(new Error("COA.Create", "Head code cannot be null or empty."));
        }

        try
        {
            var coa = new COA(headCode, headName, isGl, type);
            coa.RaiseDomainEvent(new CreateFirstLevelCoaDomainEvent(coa.HeadName, coa.Type));
            return coa;
        }
        catch (Exception ex)
        {
            return Result.Failure<COA>(new Error("COA.Create", $"Failed to create COA: {ex.Message}"));
        }
    }

    private static string GenerateNextHeadCode(COA parentCoa)
    {
        Guard.Against.Null(parentCoa, nameof(parentCoa));

        var parentChildCodes = parentCoa._coas
            .Where(coa => coa.HeadCode.Length == parentCoa.HeadCode.Length + 2)
            .Select(coa => int.Parse(coa.HeadCode.Substring(parentCoa.HeadCode.Length)))
            .ToList();

        int nextChildCode = parentChildCodes.Any() ? parentChildCodes.Max() + 1 : 1;
        return $"{parentCoa.HeadCode}{nextChildCode:D2}";
    }

    public void InsertChildrens(List<COA> childCOAs)
    {
        Guard.Against.Null(childCOAs, nameof(childCOAs));
        _coas.AddRange(childCOAs);
    }
}
