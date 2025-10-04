using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CatControl.API.DTOs.Wishlist;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;
    
    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserWishlist()
    {
        var userId = GetUserId();
        var wishlist = await _wishlistService.GetUserWishlist(userId);
        return Ok(wishlist);
    }
    
    [HttpGet("priority/{prioridade}")]
    public async Task<IActionResult> GetWishlistByPriority(string prioridade)
    {
        var userId = GetUserId();
        var wishlist = await _wishlistService.GetWishlistByPriority(userId, prioridade);
        return Ok(wishlist);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWishlistById(int id)
    {
        var userId = GetUserId();
        var wishlist = await _wishlistService.GetWishlistById(id, userId);
        
        if (wishlist == null)
        {
            return NotFound(new { message = "Item da wishlist não encontrado" });
        }
        
        return Ok(wishlist);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWishlistItem([FromBody] CreateWishlistDto createWishlistDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var wishlist = await _wishlistService.CreateWishlistItem(createWishlistDto, userId);
        
        if (wishlist == null)
        {
            return BadRequest(new { message = "Erro ao criar item da wishlist" });
        }
        
        return CreatedAtAction(nameof(GetWishlistById), new { id = wishlist.Id }, wishlist);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWishlistItem(int id, [FromBody] UpdateWishlistDto updateWishlistDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var wishlist = await _wishlistService.UpdateWishlistItem(id, updateWishlistDto, userId);
        
        if (wishlist == null)
        {
            return NotFound(new { message = "Item da wishlist não encontrado" });
        }
        
        return Ok(wishlist);
    }
    
    [HttpPost("{id}/purchase")]
    public async Task<IActionResult> MarkAsPurchased(int id)
    {
        var userId = GetUserId();
        var wishlist = await _wishlistService.MarkAsPurchased(id, userId);
        
        if (wishlist == null)
        {
            return NotFound(new { message = "Item da wishlist não encontrado" });
        }
        
        return Ok(wishlist);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWishlistItem(int id)
    {
        var userId = GetUserId();
        var result = await _wishlistService.DeleteWishlistItem(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Item da wishlist não encontrado" });
        }
        
        return NoContent();
    }
}
