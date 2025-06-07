using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Core.Contracts.Services;

public interface ICheckListService
{
    Task<List<CheckListListsDto>> GetAllCheckListsAsync(CancellationToken cancellationToken);
    Task<bool> CheckCheckListExist(int checkListId, CancellationToken cancellationToken);
    Task<Result> AddCheckList(AddCheckListDto checkList, CancellationToken cancellationToken);
    Task<Result> UpdateCheckList(UpdateCheckListDto dto, CancellationToken cancellationToken);
    Task<CheckList?> GetCheckListById(int checkListId, CancellationToken cancellationToken);

}
