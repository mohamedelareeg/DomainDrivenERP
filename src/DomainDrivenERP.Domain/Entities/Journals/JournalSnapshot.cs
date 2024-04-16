using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Transactions;
using DomainDrivenERP.Domain.Enums;

namespace DomainDrivenERP.Domain.Entities.Journals;
public class JournalSnapshot
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public bool IsOpening { get; set; }
    public DateTime JournalDate { get; set; }
    public JournalStatus Status { get; set; }
    public List<TransactionSnapshot> Transactions { get; set; } = new List<TransactionSnapshot>();
}
