using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using CleanArchitectureWithDDD.Domain.Entities.Transactions;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs;

public sealed class COA : AggregateRoot
{
    private readonly List<COA> _coas = new();
    private readonly List<Transaction> _transactions = new();
    public COA()
    {

    }
    public COA(string headCode, string headName, string parentHeadCode, bool isGl, COA_Type type, int headLevel)
    {
        HeadCode = headCode;
        HeadName = headName;
        ParentHeadCode = parentHeadCode;
        IsGl = isGl;
        Type = type;
        HeadLevel = headLevel;
    }
    public COA(string headCode, string headName, bool isGl, COA_Type type)
    {
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
            coa.RaiseDomainEvent(new CreateCOADomainEvent(Guid.NewGuid(), headName, parentCoa.HeadCode, parentCoa.Type));

            return coa;
        }
        catch (Exception ex)
        {
            return Result.Failure<COA>(new Error("COA.Create", $"Failed to create COA: {ex.Message}"));
        }
    }
    public static Result<COA> Create(string headName, string headCode, COA_Type type, bool isGl = false)
    {
        if (string.IsNullOrEmpty(headCode))
        {
            return Result.Failure<COA>(new Error("COA.Create", "Head code cannot be null or empty."));
        }

        try
        {
            var coa = new COA(headCode, headName, isGl, type);
            coa.RaiseDomainEvent(new CreateFirstLevelCoaDomainEvent(Guid.NewGuid(), coa.HeadName, coa.Type));
            return coa;
        }
        catch (Exception ex)
        {
            return Result.Failure<COA>(new Error("COA.Create", $"Failed to create COA: {ex.Message}"));
        }
    }

    private static string GenerateNextHeadCode(COA parentCoa)
    {
        var parentChildCodes = parentCoa._coas
            .Where(coa => coa.HeadCode.Length == parentCoa.HeadCode.Length + 2)
            .Select(coa => int.Parse(coa.HeadCode.Substring(parentCoa.HeadCode.Length)))
            .ToList();

        int nextChildCode = parentChildCodes.Any() ? parentChildCodes.Max() + 1 : 1;
        return $"{parentCoa.HeadCode}{nextChildCode:D2}";
    }

    public void InsertChildrens(List<COA> childCOAs)
    {
        _coas.AddRange(childCOAs);
    }
}
