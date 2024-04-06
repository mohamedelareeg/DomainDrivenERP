using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Dtos;
public class TransactionDto
{
    public double Debit { get; set; }
    public double Credit { get; set; }
    public string? AccountName { get; set; }
    public string? AccountHeadCode { get; set; }
}
