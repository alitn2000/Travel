using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListTripDtos;

namespace Travel.Domain.Service.CheckListTrips.Commands;

public class UpdateIsCheckedCommand : IRequest<Result>
{
    public UpdateCheckListTripDto Dto { get; set; }
    public int UserId { get; set; }

    public UpdateIsCheckedCommand(UpdateCheckListTripDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
