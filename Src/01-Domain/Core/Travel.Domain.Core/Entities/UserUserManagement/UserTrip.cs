using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Entities.TripManagement;

namespace Travel.Domain.Core.Entities.UserUserManagement;

public class UserTrip : BaseEntity
{
    private UserTrip()
    {
        

    }

    public UserTrip(int id, User user, int userId, Trip trip, int tripId, bool isOwner)
    {
        Id = id;
        User = user;
        UserId = userId;
        Trip = trip;
        TripId = tripId;
        IsOwner = isOwner;
    }

    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Trip Trip { get; set; }
    public int TripId { get; set; }
    public bool IsOwner { get; set; } = false;
}
