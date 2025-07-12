using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Features.Commands.CheckLists.AddCheckList;
using Travel.Domain.Service.Features.Commands.CheckLists.UpdateCheckList;
using Travel.Domain.Service.Features.Queries.Checklists.GetAllCheckListsAsync;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers;

public class CheckListController : BaseController
{

    public CheckListController(IMediator mediator) : base(mediator) { }

    [HttpGet("GetAllCheckLists")]
     public async Task<ActionResult<List<CheckListListsDto>>> GetAllCheckLists(CancellationToken cancellationToken)
    {
        var checkLists = await _mediator.Send(new GetAllCheckListsAsyncQuery(),cancellationToken);
        if(checkLists is null)
        {
            return NotFound("No checklists found.");
        }
        return Ok(checkLists);
    }
    [HttpPost("AddCheckList")]
    public async Task<ActionResult<Result>> AddCheckList(AddCheckListDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = GetCurrentUserId();
        if (userId == 0)
        {
            return Unauthorized("User not authenticated.");
        }
        var result = await _mediator.Send(new AddCheckListCommand(dto,userId),cancellationToken);
        if (!result.Flag)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }

    [HttpPatch("UpdateCheckList")]
    public async Task<ActionResult<Result>> UpdateCheckList(UpdateCheckListDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = GetCurrentUserId();
        if (userId == 0)
        {
            return Unauthorized("User not authenticated.");
        }
        var result = await _mediator.Send(new UpdateCheckListCommand(dto,userId),cancellationToken);
        if (!result.Flag)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }
}
