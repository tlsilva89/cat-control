using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CatControl.API.DTOs.Finance;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService _financeService;
    
    public FinanceController(IFinanceService financeService)
    {
        _financeService = financeService;
    }
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserFinances()
    {
        var userId = GetUserId();
        var finances = await _financeService.GetUserFinances(userId);
        return Ok(finances);
    }
    
    [HttpGet("period")]
    public async Task<IActionResult> GetFinancesByPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var userId = GetUserId();
        var finances = await _financeService.GetFinancesByPeriod(userId, startDate, endDate);
        return Ok(finances);
    }
    
    [HttpGet("category/{categoria}")]
    public async Task<IActionResult> GetFinancesByCategory(string categoria)
    {
        var userId = GetUserId();
        var finances = await _financeService.GetFinancesByCategory(userId, categoria);
        return Ok(finances);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFinanceById(int id)
    {
        var userId = GetUserId();
        var finance = await _financeService.GetFinanceById(id, userId);
        
        if (finance == null)
        {
            return NotFound(new { message = "Registro financeiro não encontrado" });
        }
        
        return Ok(finance);
    }
    
    [HttpGet("summary")]
    public async Task<IActionResult> GetFinanceSummary([FromQuery] int? year, [FromQuery] int? month)
    {
        var userId = GetUserId();
        var summary = await _financeService.GetFinanceSummary(userId, year, month);
        return Ok(summary);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFinance([FromBody] CreateFinanceDto createFinanceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var finance = await _financeService.CreateFinance(createFinanceDto, userId);
        
        if (finance == null)
        {
            return BadRequest(new { message = "Erro ao criar registro financeiro" });
        }
        
        return CreatedAtAction(nameof(GetFinanceById), new { id = finance.Id }, finance);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFinance(int id, [FromBody] UpdateFinanceDto updateFinanceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var finance = await _financeService.UpdateFinance(id, updateFinanceDto, userId);
        
        if (finance == null)
        {
            return NotFound(new { message = "Registro financeiro não encontrado" });
        }
        
        return Ok(finance);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFinance(int id)
    {
        var userId = GetUserId();
        var result = await _financeService.DeleteFinance(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Registro financeiro não encontrado" });
        }
        
        return NoContent();
    }
}
