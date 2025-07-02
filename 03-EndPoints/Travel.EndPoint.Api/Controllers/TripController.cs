using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;
using Travel.Domain.Service.Trips.Commands;
using Travel.Domain.Service.Trips.Queries;
using Travel.Domain.Services.AppService;
using Travel.EndPoint.Api.Controllers.Base;

namespace Travel.EndPoint.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TripController : BaseController
{
    private readonly IMediator _mediator;

    public TripController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
    public async Task<ActionResult<UpdateTripDto>> UpdateTrip(UpdateTripDto dto, CancellationToken cancellationToken)
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
    public async Task<ActionResult<AddUsersToTripDto>> AddUsersToTrip(AddUsersToTripDto dto, CancellationToken cancellationToken)
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
