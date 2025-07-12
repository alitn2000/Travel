using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Commit(int currentUserId, CancellationToken cancellationToken)
    {
        var entries = _context.ChangeTracker
                               .Entries()
                               .Where(e => e.Entity is BaseEntity &&(e.State == EntityState.Added || e.State == EntityState.Modified));
        foreach (var entry in entries)
        {
            
            var entity = (BaseEntity)entry.Entity;
            if(entry.State == EntityState.Modified)
            {
                entity.UpdateDate = DateTime.UtcNow;
            }
           

            if (entry.State == EntityState.Added)
            {
                entity.CreateDate = DateTime.UtcNow;
                entity.CreatedUserId = currentUserId;
            }
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }

    private void Commit()
    {


        //loop through all DbContext and save changes


        //SaveChange EF
    }
}
