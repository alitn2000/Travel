using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListDtos;

namespace Travel.Domain.Service.Features.Commands.CheckLists.AddCheckList;

public class AddCheckListCommand : IRequest<Result>
{
    public AddCheckListDto Dto { get; set; }
    public int UserId { get; set; }
    public AddCheckListCommand(AddCheckListDto dto, int userId)
    {
        Dto = dto;
        UserId = userId;
    }
}
