﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Extentions;
public static class CustomListExtensions
{
    public static CustomList<T> ToCustomList<T>(this IEnumerable<T> items, int? totalCount = null, int? totalPages = null)
    {
        return new CustomList<T>(items.ToList(), totalCount, totalPages);
    }
    public static async Task<CustomList<T>> ToCustomListAsync<T>(this Task<IEnumerable<T>> task, int? totalCount = null, int? totalPages = null)
    {
        IEnumerable<T> items = await task;
        return items.ToCustomList(totalCount, totalPages);
    }
}