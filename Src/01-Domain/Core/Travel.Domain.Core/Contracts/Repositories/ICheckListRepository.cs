using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Contracts.Repositories;

 public interface ICheckListRepository
{
    Task<List<CheckListListsDto>> GetAllCheckListsAsync(CancellationToken cancellationToken);
    Task<bool> CheckCheckListExist(int checkListId, CancellationToken cancellationToken);
    Task<bool> AddCheckList(CheckList checkList,int userId, CancellationToken cancellationToken);
    Task<bool> CheckTypesExists(string chListEnum, TripEnums tripEnum, CancellationToken cancellationToken);
    Task<Result> UpdateCheckList(UpdateCheckListDto dto, int userId, CancellationToken cancellationToken);
    Task<CheckList?> GetCheckListById(int checkListId, CancellationToken cancellationToken);
}
