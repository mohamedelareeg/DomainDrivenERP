using System.Reflection;

namespace CleanArchitectureWithDDD.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
