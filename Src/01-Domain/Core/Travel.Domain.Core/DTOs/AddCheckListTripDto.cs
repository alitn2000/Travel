using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs;

public class AddCheckListTripDto
{
    public int TripId { get; set; }
    public int CheckListId { get; set; }
}
