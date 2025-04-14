using BLL.DTO;
using BLL.Helpers;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficeController(OfficeService officeService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<OfficeDto>> CreateOfficeAsync([FromForm] OfficeForCreatingDto officeDto, CancellationToken cancellationToken) =>
        Ok(await officeService.CreateOfficeAsync(officeDto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<OfficeDto>>> GetOfficesAsync(CancellationToken cancellationToken) =>
        Ok(await officeService.GetOfficesAsync(cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OfficeDto>> GetOfficeByIdAsync([FromRoute] string id, CancellationToken cancellationToken) =>
        Ok(await officeService.GetOfficeByIdAsync(id, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OfficeDto>> UpdateOfficeAsync([FromRoute] string id, [FromForm] OfficeForUpdatingDto dto, CancellationToken cancellationToken) =>
        Ok(await officeService.UpdateOfficeAsync(id, dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id}")]
    public async Task<ActionResult<OfficeDto>> UpdateOfficeStatusAsync([FromRoute] string id, [FromForm] OfficeForChangingStatusDto dto, CancellationToken cancellationToken) =>
        Ok(await officeService.UpdateOfficeStatusAsync(id, dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOfficeAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        await officeService.DeleteOfficeAsync(id, cancellationToken);
        return Ok(Messages.OfficeDeletedSuccessfully);
    }
 }