using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.Profile;

namespace Travel.Domain.Service.Profiles.Commands;

public class UpdateProfileCommand : IRequest<Result>
{
    public UpdateProfileDto Dto { get; set; }
    public int UserId { get; set; }
    public UpdateProfileCommand(UpdateProfileDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
