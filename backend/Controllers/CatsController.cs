using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CatControl.API.DTOs.Cat;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CatsController : ControllerBase
{
    private readonly ICatService _catService;
    
    public CatsController(ICatService catService)
    {
        _catService = catService;
    }
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserCats()
    {
        var userId = GetUserId();
        var cats = await _catService.GetUserCats(userId);
        return Ok(cats);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCatById(int id)
    {
        var userId = GetUserId();
        var cat = await _catService.GetCatById(id, userId);
        
        if (cat == null)
        {
            return NotFound(new { message = "Gato não encontrado" });
        }
        
        return Ok(cat);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCat([FromBody] CreateCatDto createCatDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var cat = await _catService.CreateCat(createCatDto, userId);
        
        if (cat == null)
        {
            return BadRequest(new { message = "Erro ao criar gato" });
        }
        
        return CreatedAtAction(nameof(GetCatById), new { id = cat.Id }, cat);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCat(int id, [FromBody] UpdateCatDto updateCatDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var cat = await _catService.UpdateCat(id, updateCatDto, userId);
        
        if (cat == null)
        {
            return NotFound(new { message = "Gato não encontrado" });
        }
        
        return Ok(cat);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCat(int id)
    {
        var userId = GetUserId();
        var result = await _catService.DeleteCat(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Gato não encontrado" });
        }
        
        return NoContent();
    }
}
