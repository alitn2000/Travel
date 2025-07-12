using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;

namespace Travel.Domain.Core.Entities;

public class CheckListTrip : BaseEntity
{
    public int Id { get; set; }
    public bool IsChecked { get; set; } = false;

    public int TripId { get; set; }
    public Trip Trip { get; set; }
    public int CheckListId { get; set; }
    public CheckList CheckList { get; set; }

}
