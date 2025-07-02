using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.CheckLists.Commands;
using Travel.Domain.Service.CheckLists.Queries;

namespace Travel.EndPoint.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CheckListController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckListController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
    public async Task<ActionResult<AddCheckListDto>> AddCheckList(AddCheckListDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(new AddCheckListCommand(dto),cancellationToken);
        if (!result.Flag)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }

    [HttpPatch("UpdateCheckList")]
    public async Task<ActionResult<UpdateCheckListDto>> UpdateCheckList(UpdateCheckListDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _mediator.Send(new UpdateCheckListCommand(dto),cancellationToken);
        if (!result.Flag)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }
}
