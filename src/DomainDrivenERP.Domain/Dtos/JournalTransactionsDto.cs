using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Domain.Dtos;
public class JournalTransactionsDto
{
    public Guid TransactionId { get; set; }
    public Guid JournalId { get; set; }
    public double Debit { get; set; }
    public double Credit { get; set; }
    public string? AccountName { get; set; }
    public string? AccountHeadCode { get; set; }
}
