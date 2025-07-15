using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Entities.CheckListManagement;

namespace Travel.Domain.Service.Features.Commands.CheckLists.AddCheckList
{
    public class AddCheckListHandler : IRequestHandler<AddCheckListCommand, Result>
    {
        private readonly ICheckListRepository _checkListRepository;

        public AddCheckListHandler(ICheckListRepository checkListRepository)
        {
            _checkListRepository = checkListRepository;
        }

        public async Task<Result> Handle(AddCheckListCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var userId = request.UserId;
            var result = await _checkListRepository.CheckTypesExists(dto.ChekListType, dto.TripType, cancellationToken);


            if (result)
                return new Result(false, "check list with this types is exists!!!");

            var checkList = new CheckList(dto.ChekListType, dto.TripType);

            if (await _checkListRepository.AddCheckList(checkList, userId, cancellationToken))
                return new Result(true, "check list added successfully!!!");

            return new Result(false, "check list not added!!!");
        }
    }
}
