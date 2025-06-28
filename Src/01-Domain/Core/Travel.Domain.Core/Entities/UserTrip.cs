using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Entities;

public class UserTrip
{
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Trip Trip { get; set; }
    public int TripId { get; set; }
    public bool IsOwner { get; set; } = false;
}
