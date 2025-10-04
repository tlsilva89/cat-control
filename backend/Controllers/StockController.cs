using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CatControl.API.DTOs.Stock;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;
    
    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserStock()
    {
        var userId = GetUserId();
        var stocks = await _stockService.GetUserStock(userId);
        return Ok(stocks);
    }
    
    [HttpGet("category/{categoria}")]
    public async Task<IActionResult> GetStockByCategory(string categoria)
    {
        var userId = GetUserId();
        var stocks = await _stockService.GetStockByCategory(userId, categoria);
        return Ok(stocks);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockById(int id)
    {
        var userId = GetUserId();
        var stock = await _stockService.GetStockById(id, userId);
        
        if (stock == null)
        {
            return NotFound(new { message = "Item de estoque não encontrado" });
        }
        
        return Ok(stock);
    }
    
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock()
    {
        var userId = GetUserId();
        var stocks = await _stockService.GetLowStock(userId);
        return Ok(stocks);
    }
    
    [HttpGet("expiring")]
    public async Task<IActionResult> GetExpiringStock([FromQuery] int days = 30)
    {
        var userId = GetUserId();
        var stocks = await _stockService.GetExpiringStock(userId, days);
        return Ok(stocks);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockDto createStockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var stock = await _stockService.CreateStock(createStockDto, userId);
        
        if (stock == null)
        {
            return BadRequest(new { message = "Erro ao criar item de estoque" });
        }
        
        return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto updateStockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var stock = await _stockService.UpdateStock(id, updateStockDto, userId);
        
        if (stock == null)
        {
            return NotFound(new { message = "Item de estoque não encontrado" });
        }
        
        return Ok(stock);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock(int id)
    {
        var userId = GetUserId();
        var result = await _stockService.DeleteStock(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Item de estoque não encontrado" });
        }
        
        return NoContent();
    }
}
