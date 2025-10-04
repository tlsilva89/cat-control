using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CatControl.API.DTOs.Vaccine;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VaccinesController : ControllerBase
{
    private readonly IVaccineService _vaccineService;
    
    public VaccinesController(IVaccineService vaccineService)
    {
        _vaccineService = vaccineService;
    }
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserVaccines()
    {
        var userId = GetUserId();
        var vaccines = await _vaccineService.GetUserVaccines(userId);
        return Ok(vaccines);
    }
    
    [HttpGet("cat/{catId}")]
    public async Task<IActionResult> GetCatVaccines(int catId)
    {
        var userId = GetUserId();
        var vaccines = await _vaccineService.GetCatVaccines(catId, userId);
        return Ok(vaccines);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVaccineById(int id)
    {
        var userId = GetUserId();
        var vaccine = await _vaccineService.GetVaccineById(id, userId);
        
        if (vaccine == null)
        {
            return NotFound(new { message = "Vacina não encontrada" });
        }
        
        return Ok(vaccine);
    }
    
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingVaccines([FromQuery] int days = 30)
    {
        var userId = GetUserId();
        var vaccines = await _vaccineService.GetUpcomingVaccines(userId, days);
        return Ok(vaccines);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateVaccine([FromBody] CreateVaccineDto createVaccineDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var vaccine = await _vaccineService.CreateVaccine(createVaccineDto, userId);
        
        if (vaccine == null)
        {
            return BadRequest(new { message = "Erro ao criar vacina ou gato não encontrado" });
        }
        
        return CreatedAtAction(nameof(GetVaccineById), new { id = vaccine.Id }, vaccine);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVaccine(int id, [FromBody] UpdateVaccineDto updateVaccineDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var vaccine = await _vaccineService.UpdateVaccine(id, updateVaccineDto, userId);
        
        if (vaccine == null)
        {
            return NotFound(new { message = "Vacina não encontrada" });
        }
        
        return Ok(vaccine);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVaccine(int id)
    {
        var userId = GetUserId();
        var result = await _vaccineService.DeleteVaccine(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Vacina não encontrada" });
        }
        
        return NoContent();
    }
}
