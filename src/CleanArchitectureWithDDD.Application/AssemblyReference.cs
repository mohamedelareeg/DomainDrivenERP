﻿using System.Reflection;

namespace CleanArchitectureWithDDD.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
