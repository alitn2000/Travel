using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.TripDtos;

namespace Travel.Domain.Service.Trips.Commands;

public class AddTripCommand : IRequest<Result>
{
    public AddTripDto Dto { get; set; }
    public int UserId { get; set; }

    public AddTripCommand(AddTripDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
