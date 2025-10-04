using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Wishlist;
using CatControl.API.Models;

namespace CatControl.API.Services;

public class WishlistService : IWishlistService
{
    private readonly AppDbContext _context;
    
    public WishlistService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<WishlistDto>> GetUserWishlist(int userId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Cat)
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.Prioridade == "Alta")
            .ThenByDescending(w => w.Prioridade == "Média")
            .ThenBy(w => w.Comprado)
            .ThenByDescending(w => w.CreatedAt)
            .ToListAsync();
        
        return wishlist.Select(w => MapToWishlistDto(w));
    }
    
    public async Task<IEnumerable<WishlistDto>> GetWishlistByPriority(int userId, string prioridade)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Cat)
            .Where(w => w.UserId == userId && w.Prioridade == prioridade)
            .OrderBy(w => w.Comprado)
            .ThenByDescending(w => w.CreatedAt)
            .ToListAsync();
        
        return wishlist.Select(w => MapToWishlistDto(w));
    }
    
    public async Task<WishlistDto?> GetWishlistById(int wishlistId, int userId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Cat)
            .FirstOrDefaultAsync(w => w.Id == wishlistId && w.UserId == userId);
        
        if (wishlist == null) return null;
        
        return MapToWishlistDto(wishlist);
    }
    
    public async Task<WishlistDto?> CreateWishlistItem(CreateWishlistDto createWishlistDto, int userId)
    {
        if (createWishlistDto.CatId.HasValue)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == createWishlistDto.CatId.Value && c.UserId == userId);
            if (cat == null) return null;
        }
        
        var wishlist = new Wishlist
        {
            UserId = userId,
            CatId = createWishlistDto.CatId,
            NomeProduto = createWishlistDto.NomeProduto,
            Categoria = createWishlistDto.Categoria,
            PrecoEstimado = createWishlistDto.PrecoEstimado,
            Prioridade = createWishlistDto.Prioridade,
            LinkProduto = createWishlistDto.LinkProduto,
            Loja = createWishlistDto.Loja,
            Observacoes = createWishlistDto.Observacoes,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();
        
        await _context.Entry(wishlist).Reference(w => w.Cat).LoadAsync();
        
        return MapToWishlistDto(wishlist);
    }
    
    public async Task<WishlistDto?> UpdateWishlistItem(int wishlistId, UpdateWishlistDto updateWishlistDto, int userId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Cat)
            .FirstOrDefaultAsync(w => w.Id == wishlistId && w.UserId == userId);
        
        if (wishlist == null) return null;
        
        if (updateWishlistDto.CatId.HasValue)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == updateWishlistDto.CatId.Value && c.UserId == userId);
            if (cat == null) return null;
            wishlist.CatId = updateWishlistDto.CatId;
        }
        
        wishlist.NomeProduto = updateWishlistDto.NomeProduto ?? wishlist.NomeProduto;
        wishlist.Categoria = updateWishlistDto.Categoria ?? wishlist.Categoria;
        wishlist.PrecoEstimado = updateWishlistDto.PrecoEstimado ?? wishlist.PrecoEstimado;
        wishlist.Prioridade = updateWishlistDto.Prioridade ?? wishlist.Prioridade;
        wishlist.LinkProduto = updateWishlistDto.LinkProduto ?? wishlist.LinkProduto;
        wishlist.Loja = updateWishlistDto.Loja ?? wishlist.Loja;
        wishlist.Comprado = updateWishlistDto.Comprado ?? wishlist.Comprado;
        wishlist.DataCompra = updateWishlistDto.DataCompra ?? wishlist.DataCompra;
        wishlist.Observacoes = updateWishlistDto.Observacoes ?? wishlist.Observacoes;
        wishlist.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        await _context.Entry(wishlist).Reference(w => w.Cat).LoadAsync();
        
        return MapToWishlistDto(wishlist);
    }
    
    public async Task<bool> DeleteWishlistItem(int wishlistId, int userId)
    {
        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.Id == wishlistId && w.UserId == userId);
        
        if (wishlist == null) return false;
        
        _context.Wishlists.Remove(wishlist);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<WishlistDto?> MarkAsPurchased(int wishlistId, int userId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Cat)
            .FirstOrDefaultAsync(w => w.Id == wishlistId && w.UserId == userId);
        
        if (wishlist == null) return null;
        
        wishlist.Comprado = true;
        wishlist.DataCompra = DateTime.UtcNow;
        wishlist.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return MapToWishlistDto(wishlist);
    }
    
    private WishlistDto MapToWishlistDto(Wishlist wishlist)
    {
        return new WishlistDto
        {
            Id = wishlist.Id,
            CatId = wishlist.CatId,
            CatNome = wishlist.Cat?.Nome,
            NomeProduto = wishlist.NomeProduto,
            Categoria = wishlist.Categoria,
            PrecoEstimado = wishlist.PrecoEstimado,
            Prioridade = wishlist.Prioridade,
            LinkProduto = wishlist.LinkProduto,
            Loja = wishlist.Loja,
            Comprado = wishlist.Comprado,
            DataCompra = wishlist.DataCompra,
            Observacoes = wishlist.Observacoes
        };
    }
}
