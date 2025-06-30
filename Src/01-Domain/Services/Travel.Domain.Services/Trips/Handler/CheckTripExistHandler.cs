using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Trips.Queries;

namespace Travel.Domain.Service.Trips.Handler
{
    public class CheckTripExistHandler : IRequestHandler<CheckTripExistQuery, bool>
    {
        private readonly ITripRepository _tripRepository;

        public CheckTripExistHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<bool> Handle(CheckTripExistQuery request, CancellationToken cancellationToken)
              => await _tripRepository.CheckTripExist(request.TripId, cancellationToken);
    }
}
