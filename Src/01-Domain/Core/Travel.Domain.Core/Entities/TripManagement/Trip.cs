using System.Collections.Generic;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.BaseContracts;
using Travel.Domain.Core.Entities.CheckListManagement;
using Travel.Domain.Core.Entities.UserManagement;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities.TripManagement;

public class Trip : BaseEntity, IAggrigateRoot
{
    public int Id { get; private set; }
    public string Destination { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public TripEnums TripType { get; private set; }
    public StatusEnum Status { get; private set; } = StatusEnum.Pending;

    public List<CheckListTrip> CheckListTrips { get; private set; }
    public List<UserTrip> USerTrips { get; private set; } = new List<UserTrip>();


    public Trip()
    {

    }

    public Trip(string destination, DateTime start, DateTime end, TripEnums tripType)
    {

        Destination = destination;
        Start = start;
        End = end;
        TripType = tripType;
        Status = StatusEnum.Pending;
    }

    public Result  UpdateTrip(string destination, DateTime start, DateTime end, TripEnums tripType)
    {

        var validationResult = TripValidation(start, end, tripType);

        if (!validationResult.Flag)
            return validationResult;

        Destination = destination;
        Start = start;
        End = end;
        TripType = tripType;
        

        return new Result(true,"Successfull");
    }

    public void UpdateStatus(StatusEnum status)
    {
        Status = status;
    }

    public Result TripValidation(DateTime start, DateTime end, TripEnums tripType)
    {
        if (start < DateTime.UtcNow)
            return new Result(false, "Start date cannot be in the past.");

        if (start >= end)
            return new Result(false, "Start date must be before end date.");

        if (!Enum.IsDefined(typeof(TripEnums), tripType))
            return new Result(false, "Invalid trip type.");

        return new Result(true, "Ok");
    }
}
