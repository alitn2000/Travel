using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Entities.CheckListManagement;
using Travel.Domain.Core.Entities.UserUserManagement;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities.TripManagement;

public class Trip : BaseEntity, IAggregateRoot
{
    public int Id { get; private set; }
    public string Destination { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public TripEnums  TripType { get; private set; }
    public StatusEnum Status { get; private set; } = StatusEnum.Pending;

    public List<CheckListTrip> CheckListTrips { get; private set; } 
    public List<UserTrip> USerTrips { get; private set; } = new List<UserTrip>();


    public Trip()
    {
        
    }

    public Trip(string destination, DateTime start, DateTime end, TripEnums tripType, StatusEnum status)
    {
        Destination = destination;
        Start = start;
        End = end;
        TripType = tripType;
        Status = status;
    }
}
