using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Vaccine;
using CatControl.API.Models;

namespace CatControl.API.Services;

public class VaccineService : IVaccineService
{
    private readonly AppDbContext _context;
    
    public VaccineService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<VaccineDto>> GetUserVaccines(int userId)
    {
        var vaccines = await _context.Vaccines
            .Include(v => v.Cat)
            .Where(v => v.Cat.UserId == userId)
            .OrderByDescending(v => v.DataAplicacao)
            .ToListAsync();
        
        return vaccines.Select(v => MapToVaccineDto(v));
    }
    
    public async Task<IEnumerable<VaccineDto>> GetCatVaccines(int catId, int userId)
    {
        var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == catId && c.UserId == userId);
        if (cat == null) return Enumerable.Empty<VaccineDto>();
        
        var vaccines = await _context.Vaccines
            .Include(v => v.Cat)
            .Where(v => v.CatId == catId)
            .OrderByDescending(v => v.DataAplicacao)
            .ToListAsync();
        
        return vaccines.Select(v => MapToVaccineDto(v));
    }
    
    public async Task<VaccineDto?> GetVaccineById(int vaccineId, int userId)
    {
        var vaccine = await _context.Vaccines
            .Include(v => v.Cat)
            .FirstOrDefaultAsync(v => v.Id == vaccineId && v.Cat.UserId == userId);
        
        if (vaccine == null) return null;
        
        return MapToVaccineDto(vaccine);
    }
    
    public async Task<VaccineDto?> CreateVaccine(CreateVaccineDto createVaccineDto, int userId)
    {
        // Verificar se o gato pertence ao usuário
        var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == createVaccineDto.CatId && c.UserId == userId);
        if (cat == null) return null;
        
        var vaccine = new Vaccine
        {
            CatId = createVaccineDto.CatId,
            TipoVacina = createVaccineDto.TipoVacina,
            DataAplicacao = createVaccineDto.DataAplicacao,
            ProximaAplicacao = createVaccineDto.ProximaAplicacao,
            LocalAplicacao = createVaccineDto.LocalAplicacao,
            Veterinario = createVaccineDto.Veterinario,
            Valor = createVaccineDto.Valor,
            Observacoes = createVaccineDto.Observacoes,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Vaccines.Add(vaccine);
        await _context.SaveChangesAsync();
        
        vaccine.Cat = cat;
        return MapToVaccineDto(vaccine);
    }
    
    public async Task<VaccineDto?> UpdateVaccine(int vaccineId, UpdateVaccineDto updateVaccineDto, int userId)
    {
        var vaccine = await _context.Vaccines
            .Include(v => v.Cat)
            .FirstOrDefaultAsync(v => v.Id == vaccineId && v.Cat.UserId == userId);
        
        if (vaccine == null) return null;
        
        vaccine.TipoVacina = updateVaccineDto.TipoVacina ?? vaccine.TipoVacina;
        vaccine.DataAplicacao = updateVaccineDto.DataAplicacao ?? vaccine.DataAplicacao;
        vaccine.ProximaAplicacao = updateVaccineDto.ProximaAplicacao ?? vaccine.ProximaAplicacao;
        vaccine.LocalAplicacao = updateVaccineDto.LocalAplicacao ?? vaccine.LocalAplicacao;
        vaccine.Veterinario = updateVaccineDto.Veterinario ?? vaccine.Veterinario;
        vaccine.Valor = updateVaccineDto.Valor ?? vaccine.Valor;
        vaccine.Observacoes = updateVaccineDto.Observacoes ?? vaccine.Observacoes;
        
        await _context.SaveChangesAsync();
        
        return MapToVaccineDto(vaccine);
    }
    
    public async Task<bool> DeleteVaccine(int vaccineId, int userId)
    {
        var vaccine = await _context.Vaccines
            .Include(v => v.Cat)
            .FirstOrDefaultAsync(v => v.Id == vaccineId && v.Cat.UserId == userId);
        
        if (vaccine == null) return false;
        
        _context.Vaccines.Remove(vaccine);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<IEnumerable<VaccineDto>> GetUpcomingVaccines(int userId, int days = 30)
    {
        var endDate = DateTime.UtcNow.AddDays(days);
        
        var vaccines = await _context.Vaccines
            .Include(v => v.Cat)
            .Where(v => v.Cat.UserId == userId 
                && v.ProximaAplicacao.HasValue 
                && v.ProximaAplicacao.Value <= endDate
                && v.ProximaAplicacao.Value >= DateTime.UtcNow)
            .OrderBy(v => v.ProximaAplicacao)
            .ToListAsync();
        
        return vaccines.Select(v => MapToVaccineDto(v));
    }
    
    private VaccineDto MapToVaccineDto(Vaccine vaccine)
    {
        int? diasParaProxima = null;
        if (vaccine.ProximaAplicacao.HasValue)
        {
            diasParaProxima = (vaccine.ProximaAplicacao.Value - DateTime.UtcNow).Days;
        }
        
        return new VaccineDto
        {
            Id = vaccine.Id,
            CatId = vaccine.CatId,
            CatNome = vaccine.Cat?.Nome ?? string.Empty,
            TipoVacina = vaccine.TipoVacina,
            DataAplicacao = vaccine.DataAplicacao,
            ProximaAplicacao = vaccine.ProximaAplicacao,
            LocalAplicacao = vaccine.LocalAplicacao,
            Veterinario = vaccine.Veterinario,
            Valor = vaccine.Valor,
            Observacoes = vaccine.Observacoes,
            DiasParaProxima = diasParaProxima
        };
    }
}
