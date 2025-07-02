using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListDtos;

namespace Travel.Domain.Service.CheckLists.Commands;

public class UpdateCheckListCommand : IRequest<Result>
{
    public UpdateCheckListDto  Dto{ get; set; }
    public UpdateCheckListCommand(UpdateCheckListDto dto)
    {
        Dto = dto;
    }
}
