using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Shared;
public class CustomList<T>
{
    public int Count => Items.Count;
    public int? TotalCount { get; set; }
    public int? TotalPages { get; set; }

    public List<T> Items { get; set; }

    public CustomList(List<T> items, int? totalCount, int? totalPages)
    {
        Items = items;
        TotalCount = totalCount;
        TotalPages = totalPages;
    }
}
