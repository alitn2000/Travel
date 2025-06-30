using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.TripDtos;

namespace Travel.Domain.Service.Trips.Queries;

public class GetUsersTripsByIdQuery : IRequest<List<GetUsersTripDto>>
{
    public int UserId { get; set; }
    public GetUsersTripsByIdQuery(int userId)
    {
        UserId = userId;
    }
}
