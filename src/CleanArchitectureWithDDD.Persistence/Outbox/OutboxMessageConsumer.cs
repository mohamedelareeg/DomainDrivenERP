using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence.Outbox
{
    public class OutboxMessageConsumer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
