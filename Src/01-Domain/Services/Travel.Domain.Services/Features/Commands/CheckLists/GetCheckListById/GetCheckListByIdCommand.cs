using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.Features.Commands.CheckLists.GetCheckListById;

public class GetCheckListByIdCommand : IRequest<CheckList?>
{
    public int CheckListId { get; set; }
        public GetCheckListByIdCommand(int checkListId)
        {
            CheckListId = checkListId;
    }
}
