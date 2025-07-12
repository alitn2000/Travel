using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Service.Features.Queries.Checklists.CheckCheckListExist;

public class CheckCheckListExistQuery : IRequest<bool>
{
    public int CheckListId { get; set; }
    public CheckCheckListExistQuery(int checkListId)
    {
        CheckListId = checkListId;
    }
}
