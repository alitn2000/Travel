using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.BaseEntities;

public class BaseEntity
{
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int CreatedUserId { get; set; }
    public BaseEntity()
    {
        CreateDate = DateTime.UtcNow;
        UpdateDate = DateTime.UtcNow;
    }
}
