using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Extentions;
public static class CustomListExtensions
{
    public static CustomList<T> ToCustomList<T>(this IEnumerable<T> items, int? totalCount = null, int? totalPages = null)
    {
        return new CustomList<T>(items.ToList(), totalCount, totalPages);
    }
    public static async Task<CustomList<T>> ToCustomListAsync<T>(this IEnumerable<T> items, int? totalCount = null, int? totalPages = null)
    {
        return await Task.FromResult(new CustomList<T>(items.ToList(), totalCount, totalPages));
    }
}
