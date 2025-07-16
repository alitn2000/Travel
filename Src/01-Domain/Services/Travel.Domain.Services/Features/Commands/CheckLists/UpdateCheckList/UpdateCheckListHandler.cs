using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Service.Exceptions;

namespace Travel.Domain.Service.Features.Commands.CheckLists.UpdateCheckList;

public class UpdateCheckListHandler : IRequestHandler<UpdateCheckListCommand, Result>
{
    private readonly ICheckListRepository _checkListRepository;

    public UpdateCheckListHandler(ICheckListRepository checkListRepository)
    {
        _checkListRepository = checkListRepository;
    }

    public async Task<Result> Handle(UpdateCheckListCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var userId = request.UserId;

        var checkList = await _checkListRepository.GetCheckListById(dto.Id, cancellationToken);

        if (checkList == null)
            throw new CommandValidationException("Check list not found.");

        checkList.UpdateCheckList(dto.ChekListType, dto.TripType);
        return await _checkListRepository.UpdateCheckList(checkList, userId, cancellationToken);
       
    }
}
