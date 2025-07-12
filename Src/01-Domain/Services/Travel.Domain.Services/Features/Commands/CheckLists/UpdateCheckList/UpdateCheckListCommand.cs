using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListDtos;

namespace Travel.Domain.Service.Features.Commands.CheckLists.UpdateCheckList;

public class UpdateCheckListCommand : IRequest<Result>
{
    public UpdateCheckListDto  Dto{ get; set; }
    public int UserId { get; set; }
    public UpdateCheckListCommand(UpdateCheckListDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
