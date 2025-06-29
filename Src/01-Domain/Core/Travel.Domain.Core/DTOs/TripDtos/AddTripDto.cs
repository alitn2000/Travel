using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.TripDtos;

public class AddTripDto
{
    [Required(ErrorMessage = "Destination is required")]
    public string Destination { get; set; }
    [Required(ErrorMessage = "start date is required")]
    public DateTime Start { get; set; }
    [Required(ErrorMessage = "end date is required")]
    public DateTime End { get; set; }
    [Required(ErrorMessage = "trip type is required")]
    public TripEnums TripType { get; set; }
}
