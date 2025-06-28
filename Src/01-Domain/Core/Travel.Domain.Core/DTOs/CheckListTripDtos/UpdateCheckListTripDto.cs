using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs.CheckListTripDtos;

public class UpdateCheckListTripDto
{
    //public int UserId { get; set; }
    public int TripId { get; set; }
    public List<CheckListIsCheckedDto> CheckLists { get; set; }
    
}
