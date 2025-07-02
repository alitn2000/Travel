using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Service.CheckLists.Commands;
using Travel.Domain.Service.CheckListTrips.Commands;
using Travel.Domain.Service.CheckListTrips.Queries;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CheckListTripController : BaseController
{
    private readonly IMediator _mediator;

    public CheckListTripController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateIsChecked")]
    public async Task<ActionResult<UpdateCheckListTripDto>> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = GetCurrentUserId();

        if (userId == 0)
            return Unauthorized("User not logged in.");

        var result = await _mediator.Send(new UpdateIsCheckedCommand(dto, userId),cancellationToken);
        if (!result.Flag)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpGet("GetAllCheckListTrips")]
    public async Task<ActionResult<List<CheckListTripListDto>>> GetAllCheckListTrips(CancellationToken cancellationToken)
    {
        var checkListTrips = await _mediator.Send(new GetAllCheckListTripsQuery(),cancellationToken);
        if (checkListTrips is null)
            return NotFound("No check list trips found.");

        return Ok(checkListTrips);
    }

    [HttpPost("AddCheckListTripToTrip")]
    public async Task<ActionResult<AddCheckListToTripDto>> AddCheckListToTrips(AddCheckListToTripDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = GetCurrentUserId();
        if (userId == 0)
            return Unauthorized("User not logged in.");

        var result = await _mediator.Send(new AddCheckListTripCommand(dto, userId),cancellationToken);
        if (!result.Flag)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }

    [HttpGet("GetAllIsCheckedLists")]
    public async Task<ActionResult<List<CheckListTripListDto>>> GetAllIsCheckedLists(CancellationToken cancellationToken)
    {
        var checkListTrips = await _mediator.Send(new GetAllIsCheckedListsQuery(),cancellationToken);
        if (checkListTrips is null)
            return NotFound("No checked lists found.");

        return Ok(checkListTrips);
    }
}
