using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.TripDtos;

public class GetUsersTripDto
{
    public int Id { get; set; }
    public string Destination { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TripEnums TripType { get; set; }
    public int UserId { get; set; }
}
