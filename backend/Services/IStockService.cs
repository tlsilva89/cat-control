using CatControl.API.DTOs.Stock;

namespace CatControl.API.Services;

public interface IStockService
{
    Task<IEnumerable<StockDto>> GetUserStock(int userId);
    Task<IEnumerable<StockDto>> GetStockByCategory(int userId, string categoria);
    Task<StockDto?> GetStockById(int stockId, int userId);
    Task<StockDto?> CreateStock(CreateStockDto createStockDto, int userId);
    Task<StockDto?> UpdateStock(int stockId, UpdateStockDto updateStockDto, int userId);
    Task<bool> DeleteStock(int stockId, int userId);
    Task<IEnumerable<StockDto>> GetLowStock(int userId);
    Task<IEnumerable<StockDto>> GetExpiringStock(int userId, int days = 30);
}
