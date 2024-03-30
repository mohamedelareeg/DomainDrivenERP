using System.Reflection;

namespace CleanArchitectureWithDDD.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
