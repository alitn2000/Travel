using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Features.Commands.Trips.AddTrip;
using Travel.Domain.Service.Features.Commands.Trips.AddUsersToTrip;
using Travel.Domain.Service.Features.Commands.Trips.UpdateTrip;
using Travel.Domain.Service.Features.Queries.Trips.GetUsersTripsById;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers;

public class TripController : BaseController
{

    public TripController(IMediator mediator) : base(mediator) { }

    [HttpPost("AddTrip")]
    public async Task<ActionResult<Result>> AddTrip(AddTripDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = GetCurrentUserId();

        if (userId == 0) 
            return Unauthorized("User not logged in.");

        var result = await _mediator.Send(new AddTripCommand(dto, userId),cancellationToken);

        if (result.Flag)
            return Ok("Trip added successfully.");

        return Ok(result.Message);
    }

    [HttpGet("UsersTrips")]
    public async Task<ActionResult<List<GetUsersTripDto>>> GetUsersTrips(int userId, CancellationToken cancellationToken)
    {
        var trips = await _mediator.Send(new GetUsersTripsByIdQuery(userId), cancellationToken);
        if (!trips.Any())
        {
            return NotFound("No trips found for this user.");
        }
        return Ok(trips);
    }

    [HttpPatch("UpdateTrip")]
    public async Task<ActionResult<Result>> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = GetCurrentUserId();
        if (userId == 0) 
            return Unauthorized("User not logged in.");

        var result = await _mediator.Send(new UpdateTripCommand(userId,dto), cancellationToken);

        if (!result.Flag)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpPatch("AddUsersToTrip")]
    public async Task<ActionResult<Result>> AddUsersToTrip(AddUsersToTripDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = GetCurrentUserId();
        if (userId == 0) 
            return Unauthorized("User not logged in.");

        var result = await _mediator.Send(new AddUsersToTripCommand(dto, userId), cancellationToken);
        if (!result.Flag)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}
