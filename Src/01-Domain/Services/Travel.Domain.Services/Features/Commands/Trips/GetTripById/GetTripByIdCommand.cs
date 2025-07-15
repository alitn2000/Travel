using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities.TripManagement;

namespace Travel.Domain.Service.Features.Commands.Trips.GetTripById;

public class GetTripByIdCommand : IRequest<Trip>
{
    public int TripId { get; set; }

    public GetTripByIdCommand(int tripId)
    {
        TripId = tripId;
    }
}
