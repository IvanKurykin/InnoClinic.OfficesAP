using BLL.DTO;
using BLL.Helpers;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficeController(OfficeService officeService) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<OfficeDto>> CreateOfficeAsync([FromBody] OfficeForCreatingDto officeDto, CancellationToken cancellationToken) =>
        Ok(await officeService.CreateOfficeAsync(officeDto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<OfficeDto>>> GetOfficesAsync(CancellationToken cancellationToken) =>
        Ok(await officeService.GetOfficesAsync(cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:objectId}")]
    public async Task<ActionResult<OfficeDto>> GetOfficeByIdAsync([FromRoute] ObjectId id, CancellationToken cancellationToken) =>
        Ok(await officeService.GetOfficeByIdAsync(id, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<OfficeDto>> UpdateOfficeAsync([FromRoute] ObjectId id, [FromBody] OfficeForUpdatingDto dto, CancellationToken cancellationToken) =>
        Ok(await officeService.UpdateOfficeAsync(id, dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<OfficeDto>> UpdateOfficeStatusAsync([FromRoute] ObjectId id, [FromBody] OfficeForChangingStatusDto dto, CancellationToken cancellationToken) =>
        Ok(await officeService.UpdateOfficeStatusAsync(id, dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:objectId}")]
    public async Task<ActionResult> DeleteOfficeAsync([FromRoute] ObjectId id, CancellationToken cancellationToken)
    {
        await officeService.DeleteOfficeAsync(id, cancellationToken);
        return Ok(Messages.OfficeDeletedSuccessfully);
    }
 }
