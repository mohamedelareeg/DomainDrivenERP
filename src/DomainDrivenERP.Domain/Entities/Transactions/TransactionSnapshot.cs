using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Domain.Entities.Transactions;
public class TransactionSnapshot
{
    public Guid TransactionId { get; set; }
    public Guid JournalId { get; set; }
    public string COAId { get; set; }
    public double Debit { get; set; }
    public double Credit { get; set; }
}
