using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.BaseContracts;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities.CheckListManagement;

public class CheckList : BaseEntity, IAggrigateRoot
{
    public int Id { get; set; }
    public string ChekListType { get;private  set; }
    public TripEnums TripType { get; private set; }

    public List<CheckListTrip> CheckListTrips { get; set; }


    public CheckList() { }

    public CheckList(string chekListType, TripEnums tripType)
    {
        ChekListType = chekListType;
        TripType = tripType;
    }

    public void UpdateCheckList(string chekListType, TripEnums tripType)
    {
        ChekListType = chekListType;
        TripType = tripType;
    }


    public Result CheckListValidation(string chekListType, TripEnums tripType)
    {
        
        if (!Enum.IsDefined(typeof(TripEnums), tripType))
            return new Result(false, "Invalid trip type.");
        return new Result(true, "Validation successful.");
    }
}
