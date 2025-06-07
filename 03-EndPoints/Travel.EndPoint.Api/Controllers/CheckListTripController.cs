using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.DTOs;

namespace Travel.EndPoint.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckListTripController : ControllerBase
{
    private readonly ICheckListTripAppService _checkListTripAppService;

    public CheckListTripController(ICheckListTripAppService checkListTripAppService)
    {
        _checkListTripAppService = checkListTripAppService;
    }

    [HttpPatch("UpdateIsChecked")]
    public async Task<IActionResult> UpdateIsChecked([FromBody] UpdateCheckListTripDto dto, CancellationToken cancellationToken)
    {
        var result = await _checkListTripAppService.UpdateIsChecked(dto, cancellationToken);
        if (!result.Flag)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}
