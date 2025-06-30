using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Service.Trips.Queries
{
    public class CheckUsersHaveTripByIdQuery : IRequest<bool>
    {
        public int UserId { get; set; }
        public int TripId { get; set; }
        public CheckUsersHaveTripByIdQuery(int userId, int tripId)
        {
            UserId = userId;
            TripId = tripId;
        }
    }
}
