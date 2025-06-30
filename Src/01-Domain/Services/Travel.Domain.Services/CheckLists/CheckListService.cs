using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;

namespace Travel.Domain.Service.CheckLists;

public class CheckListService : ICheckListService
{
    private readonly ICheckListRepository _checkListRepository;
    public CheckListService(ICheckListRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;
    }

    public async Task<Result> AddCheckList(AddCheckListDto checkList, CancellationToken cancellationToken)
    {
        var result =await _checkListRepository.CheckTypesExists(checkList.ChekListType, checkList.TripType, cancellationToken);

        if (result)
            return new Result(false, "check list with this types is exists!!!");

        var chList = new CheckList
        {
            TripType = checkList.TripType,
            ChekListType = checkList.ChekListType
        };

       if(await _checkListRepository.AddCheckList(chList, cancellationToken))
            return new Result(true, "check list added successfully!!!");

        return new Result(false, "check list not added!!!");
    }

   

    public async Task<bool> CheckCheckListExist(int checkListId, CancellationToken cancellationToken)
        => await _checkListRepository.CheckCheckListExist(checkListId, cancellationToken);

    public async Task<List<CheckListListsDto>> GetAllCheckListsAsync(CancellationToken cancellationToken)
        =>await _checkListRepository.GetAllCheckListsAsync(cancellationToken);

    public async Task<CheckList?> GetCheckListById(int checkListId, CancellationToken cancellationToken)
        => await _checkListRepository.GetCheckListById(checkListId, cancellationToken);

    public async Task<Result> UpdateCheckList(UpdateCheckListDto dto, CancellationToken cancellationToken)
    {

        var result = await _checkListRepository.UpdateCheckList(dto, cancellationToken);
        return result;
    }
}
