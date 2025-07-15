using MediatR;
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
    private readonly IMediator _mediator;
    public UnitOfWork(AppDbContext context,IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<int> Commit(int currentUserId, CancellationToken cancellationToken)
    {
        var entries = _context.ChangeTracker.Entries()
                               .Where(e => e.Entity is BaseEntity &&
                                           (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Modified)
                entity.UpdateDate = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreateDate = DateTime.UtcNow;
                entity.CreatedUserId = currentUserId;
            }
        }

        var result = await _context.SaveChangesAsync(cancellationToken);

        await DispatchDomainEventsAsync(_context, _mediator, cancellationToken);

        return result;
    }

    public async Task DispatchDomainEventsAsync(AppDbContext context, IMediator mediator, CancellationToken cancellationToken)
    {
        var domainEntities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .Select(x => x.Entity);

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        foreach (var entity in domainEntities)
            entity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);
    }

}
