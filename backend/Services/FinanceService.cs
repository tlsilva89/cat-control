using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Finance;
using CatControl.API.Models;

namespace CatControl.API.Services;

public class FinanceService : IFinanceService
{
    private readonly AppDbContext _context;
    
    public FinanceService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<FinanceDto>> GetUserFinances(int userId)
    {
        var finances = await _context.Finances
            .Include(f => f.Cat)
            .Where(f => f.UserId == userId)
            .OrderByDescending(f => f.DataGasto)
            .ToListAsync();
        
        return finances.Select(f => MapToFinanceDto(f));
    }
    
    public async Task<IEnumerable<FinanceDto>> GetFinancesByPeriod(int userId, DateTime startDate, DateTime endDate)
    {
        var finances = await _context.Finances
            .Include(f => f.Cat)
            .Where(f => f.UserId == userId 
                && f.DataGasto >= startDate 
                && f.DataGasto <= endDate)
            .OrderByDescending(f => f.DataGasto)
            .ToListAsync();
        
        return finances.Select(f => MapToFinanceDto(f));
    }
    
    public async Task<IEnumerable<FinanceDto>> GetFinancesByCategory(int userId, string categoria)
    {
        var finances = await _context.Finances
            .Include(f => f.Cat)
            .Where(f => f.UserId == userId && f.Categoria == categoria)
            .OrderByDescending(f => f.DataGasto)
            .ToListAsync();
        
        return finances.Select(f => MapToFinanceDto(f));
    }
    
    public async Task<FinanceDto?> GetFinanceById(int financeId, int userId)
    {
        var finance = await _context.Finances
            .Include(f => f.Cat)
            .FirstOrDefaultAsync(f => f.Id == financeId && f.UserId == userId);
        
        if (finance == null) return null;
        
        return MapToFinanceDto(finance);
    }
    
    public async Task<FinanceDto?> CreateFinance(CreateFinanceDto createFinanceDto, int userId)
    {
        if (createFinanceDto.CatId.HasValue)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == createFinanceDto.CatId.Value && c.UserId == userId);
            if (cat == null) return null;
        }
        
        var finance = new Finance
        {
            UserId = userId,
            CatId = createFinanceDto.CatId,
            Descricao = createFinanceDto.Descricao,
            Categoria = createFinanceDto.Categoria,
            Valor = createFinanceDto.Valor,
            DataGasto = createFinanceDto.DataGasto,
            FormaPagamento = createFinanceDto.FormaPagamento,
            Recorrente = createFinanceDto.Recorrente,
            FrequenciaRecorrencia = createFinanceDto.FrequenciaRecorrencia,
            Observacoes = createFinanceDto.Observacoes,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Finances.Add(finance);
        await _context.SaveChangesAsync();
        
        await _context.Entry(finance).Reference(f => f.Cat).LoadAsync();
        
        return MapToFinanceDto(finance);
    }
    
    public async Task<FinanceDto?> UpdateFinance(int financeId, UpdateFinanceDto updateFinanceDto, int userId)
    {
        var finance = await _context.Finances
            .Include(f => f.Cat)
            .FirstOrDefaultAsync(f => f.Id == financeId && f.UserId == userId);
        
        if (finance == null) return null;
        
        if (updateFinanceDto.CatId.HasValue)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == updateFinanceDto.CatId.Value && c.UserId == userId);
            if (cat == null) return null;
            finance.CatId = updateFinanceDto.CatId;
        }
        
        finance.Descricao = updateFinanceDto.Descricao ?? finance.Descricao;
        finance.Categoria = updateFinanceDto.Categoria ?? finance.Categoria;
        finance.Valor = updateFinanceDto.Valor ?? finance.Valor;
        finance.DataGasto = updateFinanceDto.DataGasto ?? finance.DataGasto;
        finance.FormaPagamento = updateFinanceDto.FormaPagamento ?? finance.FormaPagamento;
        finance.Recorrente = updateFinanceDto.Recorrente ?? finance.Recorrente;
        finance.FrequenciaRecorrencia = updateFinanceDto.FrequenciaRecorrencia ?? finance.FrequenciaRecorrencia;
        finance.Observacoes = updateFinanceDto.Observacoes ?? finance.Observacoes;
        finance.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        await _context.Entry(finance).Reference(f => f.Cat).LoadAsync();
        
        return MapToFinanceDto(finance);
    }
    
    public async Task<bool> DeleteFinance(int financeId, int userId)
    {
        var finance = await _context.Finances
            .FirstOrDefaultAsync(f => f.Id == financeId && f.UserId == userId);
        
        if (finance == null) return false;
        
        _context.Finances.Remove(finance);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<FinanceSummaryDto> GetFinanceSummary(int userId, int? year = null, int? month = null)
    {
        var query = _context.Finances.Where(f => f.UserId == userId);
        
        if (year.HasValue && month.HasValue)
        {
            query = query.Where(f => f.DataGasto.Year == year.Value && f.DataGasto.Month == month.Value);
        }
        else if (year.HasValue)
        {
            query = query.Where(f => f.DataGasto.Year == year.Value);
        }
        else
        {
            var now = DateTime.UtcNow;
            query = query.Where(f => f.DataGasto.Year == now.Year && f.DataGasto.Month == now.Month);
        }
        
        var finances = await query.Include(f => f.Cat).ToListAsync();
        
        var totalGasto = finances.Sum(f => f.Valor);
        
        var gastoPorCategoria = finances
            .GroupBy(f => f.Categoria)
            .ToDictionary(g => g.Key, g => g.Sum(f => f.Valor));
        
        var ultimosGastos = finances
            .OrderByDescending(f => f.DataGasto)
            .Take(10)
            .Select(f => MapToFinanceDto(f))
            .ToList();
        
        return new FinanceSummaryDto
        {
            TotalGasto = totalGasto,
            GastoMensal = totalGasto,
            GastoPorCategoria = gastoPorCategoria,
            UltimosGastos = ultimosGastos
        };
    }
    
    private FinanceDto MapToFinanceDto(Finance finance)
    {
        return new FinanceDto
        {
            Id = finance.Id,
            CatId = finance.CatId,
            CatNome = finance.Cat?.Nome,
            Descricao = finance.Descricao,
            Categoria = finance.Categoria,
            Valor = finance.Valor,
            DataGasto = finance.DataGasto,
            FormaPagamento = finance.FormaPagamento,
            Recorrente = finance.Recorrente,
            FrequenciaRecorrencia = finance.FrequenciaRecorrencia,
            Observacoes = finance.Observacoes
        };
    }
}
