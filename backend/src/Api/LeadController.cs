using Microsoft.AspNetCore.Mvc;
using backend.Application.DTOs;
using backend.Application.Services;
using backend.Domain;
using System.Threading.Tasks;

namespace backend.Controllers;

[ApiController]
[Route("leads")]
public class LeadsController(LeadService leadService): ControllerBase
{

    private readonly LeadService _leadService = leadService;

    [HttpGet]
    public async Task<ActionResult<PagedResult<LeadDto>>> GetLeads([FromQuery] string? search, [FromQuery] LeadStatus? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _leadService.GetLeadsAsync(search, status, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeadWithTasksDto>> GetLead(int id)
    {
        var result = await _leadService.GetLeadByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLead([FromBody] LeadCreateDto dto)
    {
        await _leadService.CreateLeadAsync(dto);        
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLead(int id, [FromBody] LeadUpdateDto dto)
    {
        await _leadService.UpdateLeadAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLead(int id)
    {
        await _leadService.DeleteLeadAsync(id);
        return NoContent();
    }
}