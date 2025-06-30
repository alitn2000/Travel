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
    public class CheckUsersHaveTripByIdHandler : IRequestHandler<CheckUsersHaveTripByIdQuery, bool>
    {
        private readonly ITripRepository _tripRepository;

        public CheckUsersHaveTripByIdHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async  Task<bool> Handle(CheckUsersHaveTripByIdQuery request, CancellationToken cancellationToken)
          => await _tripRepository.CheckUsersHaveTripById(request.UserId, request.TripId, cancellationToken);

    }
}
