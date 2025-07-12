using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Service.Features.Queries.Trips.CheckTripExist;

public class CheckTripExistQuery : IRequest<bool>
{
    public int TripId { get; set; }
    public CheckTripExistQuery(int tripId)
    {
        TripId = tripId;
    }
}
