using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.TripDtos;

public class UpdateTripDto
{
    [Required(ErrorMessage = "trip id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "destination name is required")]
    public string Destination { get; set; }

    [Required(ErrorMessage = "start date is required")]
    [DataType(DataType.DateTime)]
    public DateTime Start { get; set; }

    [Required(ErrorMessage = "end date is required")]
    [DataType(DataType.DateTime)]
    public DateTime End { get; set; }

    [Required(ErrorMessage = "trip type is required")]
    public TripEnums TripType { get; set; }
    [Required(ErrorMessage = "User Id is required")]
    public int UserId { get; set; }
}
