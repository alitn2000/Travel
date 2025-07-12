using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Service.Features.Commands.CheckListTrips.AddCheckListTrip;
using Travel.Domain.Service.Features.Commands.CheckListTrips.UpdateIsChecked;
using Travel.Domain.Service.Features.Queries.CheckListTrips.GetAllCheckListTrips;
using Travel.Domain.Service.Features.Queries.CheckListTrips.GetAllIsCheckedLists;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers;

public class CheckListTripController : BaseController
{

    public CheckListTripController(IMediator mediator) : base(mediator) { }

    [HttpPatch("UpdateIsChecked")]
    public async Task<ActionResult<Result>> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<Result>> AddCheckListToTrips(AddCheckListToTripDto dto, CancellationToken cancellationToken)
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
