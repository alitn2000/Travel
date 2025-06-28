using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs.TripDtos;

public class AddUsersToTripDto
{
    [Required(ErrorMessage = "trip id is required")]
    public int TripId { get; set; }
    [Required(ErrorMessage = "users is required")]
    public List<int> UsersId { get; set; }

}
