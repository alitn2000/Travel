using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities;

public class CheckList
{
    public int Id { get; set; }
    public CheckListEnums ChekListType { get; set; }
    public TripEnums TripType { get; set; }

    public List<CheckListTrip> CheckListTrips { get; set; }
}
