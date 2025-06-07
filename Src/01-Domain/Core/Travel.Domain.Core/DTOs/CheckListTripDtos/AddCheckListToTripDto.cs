using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs.CheckListTripDtos;

public class AddCheckListToTripDto
{
    public int TripId { get; set; }
    public List<int> CheckListIds { get; set; }
}
