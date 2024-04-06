using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Dtos;
public class JournalDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsOpening { get; set; }
    public DateTime JournalDate { get; set; }
    public int Status { get; set; }
    public List<TransactionDto> Transactions { get; set; }
}
