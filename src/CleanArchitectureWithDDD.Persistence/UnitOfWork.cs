using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task SaveChangesAsync(CancellationToken cancellationToken = default) 
            => _context.SaveChangesAsync(cancellationToken);
    }
}
