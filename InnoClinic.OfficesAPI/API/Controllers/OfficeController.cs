using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficeController(IOfficeService officeService) : ControllerBase
{
    private const string _officeDeletedSuccessfullyMessage = "The office was successfully deleted.";

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<OfficeResultDto>> CreateOfficeAsync([FromForm] OfficeRequestDto officeDto, CancellationToken cancellationToken) =>
        Ok(await officeService.CreateOfficeAsync(officeDto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<OfficeResultDto>>> GetOfficesAsync(CancellationToken cancellationToken) =>
        Ok(await officeService.GetOfficesAsync(cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OfficeResultDto>> GetOfficeByIdAsync([FromRoute] string id, CancellationToken cancellationToken) =>
        Ok(await officeService.GetOfficeByIdAsync(id, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OfficeResultDto>> UpdateOfficeAsync([FromRoute] string id, [FromForm] OfficeRequestDto dto, CancellationToken cancellationToken) =>
        Ok(await officeService.UpdateOfficeAsync(id, dto, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id}")]
    public async Task<ActionResult<OfficeResultDto>> UpdateOfficeStatusAsync([FromRoute] string id, [FromBody] bool isActive, CancellationToken cancellationToken) =>
        Ok(await officeService.UpdateOfficeStatusAsync(id, isActive, cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOfficeAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        await officeService.DeleteOfficeAsync(id, cancellationToken);
        return Ok(_officeDeletedSuccessfullyMessage);
    }
 }