using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListTripDtos;

namespace Travel.Domain.Service.CheckListTrips.Commands;

public class AddCheckListTripCommand : IRequest<Result>
{
    public AddCheckListToTripDto Dto { get; set; }
    public int UserId { get; set; }
    public AddCheckListTripCommand(AddCheckListToTripDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
