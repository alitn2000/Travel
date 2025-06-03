using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Domain.Core.Contracts.AppServices;

namespace Travel.EndPoint.Api.Controllers;

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
     public async Task<IActionResult> GetAllCheckLists(CancellationToken cancellationToken)
    {
        var checkLists = await _checkListAppService.GetAllCheckListsAsync(cancellationToken);
        if(checkLists is null)
        {
            return NotFound("No checklists found.");
        }
        return Ok(checkLists);
    }

}
