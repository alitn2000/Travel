using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Features.Queries.Trips.GetUsersTripsById;

public class GetUsersTripsByIdHandler : IRequestHandler<GetUsersTripsByIdQuery, List<GetUsersTripDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersTripsByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetUsersTripDto>> Handle(GetUsersTripsByIdQuery request, CancellationToken cancellationToken)
    => await _userRepository.GetUsersTripsByUserId(request.UserId, cancellationToken);
}
