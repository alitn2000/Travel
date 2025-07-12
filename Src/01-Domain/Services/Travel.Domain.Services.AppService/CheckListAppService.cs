//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Travel.Domain.Core.BaseEntities;
//using Travel.Domain.Core.Contracts.AppServices;
//using Travel.Domain.Core.Contracts.Services;
//using Travel.Domain.Core.DTOs.CheckListDtos;
//using Travel.Domain.Core.Entities;

//namespace Travel.Domain.Services.AppService
//{
//    public class CheckListAppService : ICheckListAppService
//    {
//        private readonly ICheckListService _checkListService;

//        public CheckListAppService(ICheckListService checkListService)
//        {
//            _checkListService = checkListService;
//        }

//        public async Task<Result> AddCheckList(AddCheckListDto checkList, CancellationToken cancellationToken)
//            => await _checkListService.AddCheckList(checkList, cancellationToken);

//        public async Task<bool> CheckCheckListExist(int checkListId, CancellationToken cancellationToken)
//            => await _checkListService.CheckCheckListExist(checkListId, cancellationToken);

//        public async Task<List<CheckListListsDto>> GetAllCheckListsAsync(CancellationToken cancellationToken)
//            => await _checkListService.GetAllCheckListsAsync(cancellationToken);

//        public async Task<CheckList?> GetCheckListById(int checkListId, CancellationToken cancellationToken)
//            => await _checkListService.GetCheckListById(checkListId, cancellationToken);

//        public async Task<Result> UpdateCheckList(UpdateCheckListDto dto, CancellationToken cancellationToken)
//            => await _checkListService.UpdateCheckList(dto, cancellationToken);
//    }
//}
