using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.TripDtos;

namespace Travel.Domain.Service.Trips.Commands;

public class AddUsersToTripCommand : IRequest<Result>
{
    public AddUsersToTripDto Dto { get; set; }
    public int UserId { get; set; }
    public AddUsersToTripCommand(AddUsersToTripDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
