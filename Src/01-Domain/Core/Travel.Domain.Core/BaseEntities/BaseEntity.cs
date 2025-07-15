using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Common;
using Travel.Domain.Core.Contracts.BaseContracts;

namespace Travel.Domain.Core.BaseEntities;

public abstract class BaseEntity
{
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int CreatedUserId { get; set; }
  public BaseEntity()
    {
        CreateDate = DateTime.UtcNow;
        UpdateDate = DateTime.UtcNow;
    }
    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() 
        => _domainEvents?.Clear();

    public virtual void CheckRules(IBuisnessRule rule)
    { 
        if (rule.IsBroken())
        {
            throw new ArgumentException(rule.Message);
        }
    }
}
