using CleanArchitectureWithDDD.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public Customer(
            Guid id,
            string name,
            string email,
            string phone) : base(id) { 
            Name = name;
            Email = email;
            Phone = phone;
        }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}
