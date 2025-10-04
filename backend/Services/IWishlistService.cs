using CatControl.API.DTOs.Wishlist;

namespace CatControl.API.Services;

public interface IWishlistService
{
    Task<IEnumerable<WishlistDto>> GetUserWishlist(int userId);
    Task<IEnumerable<WishlistDto>> GetWishlistByPriority(int userId, string prioridade);
    Task<WishlistDto?> GetWishlistById(int wishlistId, int userId);
    Task<WishlistDto?> CreateWishlistItem(CreateWishlistDto createWishlistDto, int userId);
    Task<WishlistDto?> UpdateWishlistItem(int wishlistId, UpdateWishlistDto updateWishlistDto, int userId);
    Task<bool> DeleteWishlistItem(int wishlistId, int userId);
    Task<WishlistDto?> MarkAsPurchased(int wishlistId, int userId);
}
