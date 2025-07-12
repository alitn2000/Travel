using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.TripDtos;

namespace Travel.Domain.Service.Features.Commands.Trips.UpdateTrip;

public class UpdateTripCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public UpdateTripDto Dto { get; set; }
    public UpdateTripCommand(int userId, UpdateTripDto dto)
    {
        UserId = userId;
        Dto = dto;
    }
}
