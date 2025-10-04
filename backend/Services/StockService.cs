using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Stock;
using CatControl.API.Models;

namespace CatControl.API.Services;

public class StockService : IStockService
{
    private readonly AppDbContext _context;
    
    public StockService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<StockDto>> GetUserStock(int userId)
    {
        var stocks = await _context.Stocks
            .Where(s => s.UserId == userId)
            .OrderBy(s => s.Categoria)
            .ThenBy(s => s.NomeProduto)
            .ToListAsync();
        
        return stocks.Select(s => MapToStockDto(s));
    }
    
    public async Task<IEnumerable<StockDto>> GetStockByCategory(int userId, string categoria)
    {
        var stocks = await _context.Stocks
            .Where(s => s.UserId == userId && s.Categoria == categoria)
            .OrderBy(s => s.NomeProduto)
            .ToListAsync();
        
        return stocks.Select(s => MapToStockDto(s));
    }
    
    public async Task<StockDto?> GetStockById(int stockId, int userId)
    {
        var stock = await _context.Stocks
            .FirstOrDefaultAsync(s => s.Id == stockId && s.UserId == userId);
        
        if (stock == null) return null;
        
        return MapToStockDto(stock);
    }
    
    public async Task<StockDto?> CreateStock(CreateStockDto createStockDto, int userId)
    {
        var stock = new Stock
        {
            UserId = userId,
            NomeProduto = createStockDto.NomeProduto,
            Categoria = createStockDto.Categoria,
            QuantidadeAtual = createStockDto.QuantidadeAtual,
            QuantidadeMinima = createStockDto.QuantidadeMinima,
            Unidade = createStockDto.Unidade,
            DataValidade = createStockDto.DataValidade,
            PrecoUnitario = createStockDto.PrecoUnitario,
            ConsumoMedioDiario = createStockDto.ConsumoMedioDiario,
            Marca = createStockDto.Marca,
            Observacoes = createStockDto.Observacoes,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Stocks.Add(stock);
        await _context.SaveChangesAsync();
        
        return MapToStockDto(stock);
    }
    
    public async Task<StockDto?> UpdateStock(int stockId, UpdateStockDto updateStockDto, int userId)
    {
        var stock = await _context.Stocks
            .FirstOrDefaultAsync(s => s.Id == stockId && s.UserId == userId);
        
        if (stock == null) return null;
        
        stock.NomeProduto = updateStockDto.NomeProduto ?? stock.NomeProduto;
        stock.Categoria = updateStockDto.Categoria ?? stock.Categoria;
        stock.QuantidadeAtual = updateStockDto.QuantidadeAtual ?? stock.QuantidadeAtual;
        stock.QuantidadeMinima = updateStockDto.QuantidadeMinima ?? stock.QuantidadeMinima;
        stock.Unidade = updateStockDto.Unidade ?? stock.Unidade;
        stock.DataValidade = updateStockDto.DataValidade ?? stock.DataValidade;
        stock.PrecoUnitario = updateStockDto.PrecoUnitario ?? stock.PrecoUnitario;
        stock.ConsumoMedioDiario = updateStockDto.ConsumoMedioDiario ?? stock.ConsumoMedioDiario;
        stock.Marca = updateStockDto.Marca ?? stock.Marca;
        stock.Observacoes = updateStockDto.Observacoes ?? stock.Observacoes;
        stock.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return MapToStockDto(stock);
    }
    
    public async Task<bool> DeleteStock(int stockId, int userId)
    {
        var stock = await _context.Stocks
            .FirstOrDefaultAsync(s => s.Id == stockId && s.UserId == userId);
        
        if (stock == null) return false;
        
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<IEnumerable<StockDto>> GetLowStock(int userId)
    {
        var stocks = await _context.Stocks
            .Where(s => s.UserId == userId && s.QuantidadeAtual <= s.QuantidadeMinima)
            .OrderBy(s => s.QuantidadeAtual)
            .ToListAsync();
        
        return stocks.Select(s => MapToStockDto(s));
    }
    
    public async Task<IEnumerable<StockDto>> GetExpiringStock(int userId, int days = 30)
    {
        var endDate = DateTime.UtcNow.AddDays(days);
        
        var stocks = await _context.Stocks
            .Where(s => s.UserId == userId 
                && s.DataValidade.HasValue 
                && s.DataValidade.Value <= endDate
                && s.DataValidade.Value >= DateTime.UtcNow)
            .OrderBy(s => s.DataValidade)
            .ToListAsync();
        
        return stocks.Select(s => MapToStockDto(s));
    }
    
    private StockDto MapToStockDto(Stock stock)
    {
        bool alertaReposicao = stock.QuantidadeAtual <= stock.QuantidadeMinima;
        
        int? diasParaVencer = null;
        if (stock.DataValidade.HasValue)
        {
            diasParaVencer = (stock.DataValidade.Value - DateTime.UtcNow).Days;
        }
        
        int? diasEstimadosDuracao = null;
        if (stock.ConsumoMedioDiario.HasValue && stock.ConsumoMedioDiario.Value > 0)
        {
            diasEstimadosDuracao = (int)(stock.QuantidadeAtual / stock.ConsumoMedioDiario.Value);
        }
        
        return new StockDto
        {
            Id = stock.Id,
            NomeProduto = stock.NomeProduto,
            Categoria = stock.Categoria,
            QuantidadeAtual = stock.QuantidadeAtual,
            QuantidadeMinima = stock.QuantidadeMinima,
            Unidade = stock.Unidade,
            DataValidade = stock.DataValidade,
            PrecoUnitario = stock.PrecoUnitario,
            ConsumoMedioDiario = stock.ConsumoMedioDiario,
            Marca = stock.Marca,
            Observacoes = stock.Observacoes,
            AlertaReposicao = alertaReposicao,
            DiasParaVencer = diasParaVencer,
            DiasEstimadosDuracao = diasEstimadosDuracao
        };
    }
}
