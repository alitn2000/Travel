using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.Entities;

namespace Travel.EndPoint.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CheckListController : ControllerBase
{
    private readonly ICheckListAppService _checkListAppService;

    public CheckListController(ICheckListAppService checkListAppService)
    {
        _checkListAppService = checkListAppService;
    }


     [HttpGet("GetAllCheckLists")]
     public async Task<ActionResult<List<CheckListListsDto>>> GetAllCheckLists(CancellationToken cancellationToken)
    {
        var checkLists = await _checkListAppService.GetAllCheckListsAsync(cancellationToken);
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

        var result = await _checkListAppService.AddCheckList(dto, cancellationToken);
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
        var result = await _checkListAppService.UpdateCheckList(dto, cancellationToken);
        if (!result.Flag)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }
}
