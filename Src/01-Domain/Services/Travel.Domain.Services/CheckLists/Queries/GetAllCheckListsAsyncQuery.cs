using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.DTOs.CheckListDtos;

namespace Travel.Domain.Service.CheckLists.Queries;

public class GetAllCheckListsAsyncQuery : IRequest<List<CheckListListsDto>>
{

}
