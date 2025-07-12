using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;

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
            var checkList = request.Dto;
            var userId = request.UserId;
            var result = await _checkListRepository.CheckTypesExists(checkList.ChekListType, checkList.TripType, cancellationToken);

            if (result)
                return new Result(false, "check list with this types is exists!!!");

            var chList = new CheckList
            {
                TripType = checkList.TripType,
                ChekListType = checkList.ChekListType
            };

            if (await _checkListRepository.AddCheckList(chList, userId, cancellationToken))
                return new Result(true, "check list added successfully!!!");

            return new Result(false, "check list not added!!!");
        }
    }
}
