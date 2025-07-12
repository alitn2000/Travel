using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities;

public class Trip : BaseEntity
{
    public int Id { get; set; }
    public string Destination { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TripEnums  TripType { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;

    public List<CheckListTrip> CheckListTrips { get; set; } 
    public List<UserTrip> USerTrips { get; set; } = new List<UserTrip>();


}
