using System.ComponentModel.DataAnnotations;
using Travel.Domain.Core.Enums;

namespace Travel.EndPoint.Api.Models.TripModels;

public class AddTripModel
{
    [Required(ErrorMessage ="destintion is required")]
    public string Destination { get; set; }
    [Required(ErrorMessage = "start date is required")]
    public DateTime Start { get; set; }
    [Required(ErrorMessage = "end date is required")]
    public DateTime End { get; set; }
    [Required(ErrorMessage = "trip type is required")]
    public TripEnums TripType { get; set; }
    [Required(ErrorMessage = "user id is required")]
    public int UserId { get; set; }
}
