using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs.TripDtos;
using Travel.Domain.Core.Entities;
using Travel.EndPoint.JobApi.Models.TripModels;

namespace Travel.EndPoint.JobApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly IUserAppService _userAppService;
    private readonly ITripAppService _tripAppService;

    public TripController(IUserAppService userAppService, ITripAppService tripAppService)
    {
        _userAppService = userAppService;
        _tripAppService = tripAppService;
    }
    [HttpPost("AddTrip")]
    public async Task<ActionResult<Result>> AddTrip(AddTripModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trip = new Trip()
        {
            Destination = model.Destination,
            End = model.End,
            Start = model.Start,
            TripType = model.TripType,
            UserId = model.UserId,

        };

        var result = await _tripAppService.AddTrip(trip, cancellationToken);

        if (result.Flag)
            return Ok("Trip added successfully.");

        return Ok(result.Message);
    }

    [HttpGet("UsersTrips")]
    public async Task<ActionResult<List<GetUsersTripDto>>> GetUsersTrips(int userId, CancellationToken cancellationToken)
    {
        var trips = await _tripAppService.GetUsersTripsById(userId, cancellationToken);
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
        var result = await _tripAppService.UpdateTrip(dto, cancellationToken);

        if (!result.Flag)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}
