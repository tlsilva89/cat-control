using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Cat;
using CatControl.API.Models;

namespace CatControl.API.Services;

public class CatService : ICatService
{
    private readonly AppDbContext _context;
    
    public CatService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CatDto>> GetUserCats(int userId)
    {
        var cats = await _context.Cats
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
        
        return cats.Select(c => MapToCatDto(c));
    }
    
    public async Task<CatDto?> GetCatById(int catId, int userId)
    {
        var cat = await _context.Cats
            .FirstOrDefaultAsync(c => c.Id == catId && c.UserId == userId);
        
        if (cat == null)
        {
            return null;
        }
        
        return MapToCatDto(cat);
    }
    
    public async Task<CatDto?> CreateCat(CreateCatDto createCatDto, int userId)
    {
        var cat = new Cat
        {
            UserId = userId,
            Nome = createCatDto.Nome,
            DataNascimento = createCatDto.DataNascimento,
            Raca = createCatDto.Raca,
            Cor = createCatDto.Cor,
            Sexo = createCatDto.Sexo,
            Castrado = createCatDto.Castrado,
            Peso = createCatDto.Peso,
            NumeroMicrochip = createCatDto.NumeroMicrochip,
            FotoUrl = createCatDto.FotoUrl,
            Observacoes = createCatDto.Observacoes,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Cats.Add(cat);
        await _context.SaveChangesAsync();
        
        return MapToCatDto(cat);
    }
    
    public async Task<CatDto?> UpdateCat(int catId, UpdateCatDto updateCatDto, int userId)
    {
        var cat = await _context.Cats
            .FirstOrDefaultAsync(c => c.Id == catId && c.UserId == userId);
        
        if (cat == null)
        {
            return null;
        }
        
        cat.Nome = updateCatDto.Nome ?? cat.Nome;
        cat.DataNascimento = updateCatDto.DataNascimento ?? cat.DataNascimento;
        cat.Raca = updateCatDto.Raca ?? cat.Raca;
        cat.Cor = updateCatDto.Cor ?? cat.Cor;
        cat.Sexo = updateCatDto.Sexo ?? cat.Sexo;
        cat.Castrado = updateCatDto.Castrado ?? cat.Castrado;
        cat.Peso = updateCatDto.Peso ?? cat.Peso;
        cat.NumeroMicrochip = updateCatDto.NumeroMicrochip ?? cat.NumeroMicrochip;
        cat.FotoUrl = updateCatDto.FotoUrl ?? cat.FotoUrl;
        cat.Observacoes = updateCatDto.Observacoes ?? cat.Observacoes;
        cat.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return MapToCatDto(cat);
    }
    
    public async Task<bool> DeleteCat(int catId, int userId)
    {
        var cat = await _context.Cats
            .FirstOrDefaultAsync(c => c.Id == catId && c.UserId == userId);
        
        if (cat == null)
        {
            return false;
        }
        
        _context.Cats.Remove(cat);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    private CatDto MapToCatDto(Cat cat)
    {
        int? idadeAnos = null;
        int? idadeMeses = null;
        
        if (cat.DataNascimento.HasValue)
        {
            var idade = DateTime.UtcNow - cat.DataNascimento.Value;
            idadeAnos = (int)(idade.TotalDays / 365.25);
            idadeMeses = (int)((idade.TotalDays % 365.25) / 30.44);
        }
        
        return new CatDto
        {
            Id = cat.Id,
            Nome = cat.Nome,
            DataNascimento = cat.DataNascimento,
            Raca = cat.Raca,
            Cor = cat.Cor,
            Sexo = cat.Sexo,
            Castrado = cat.Castrado,
            Peso = cat.Peso,
            NumeroMicrochip = cat.NumeroMicrochip,
            FotoUrl = cat.FotoUrl,
            Observacoes = cat.Observacoes,
            IdadeAnos = idadeAnos,
            IdadeMeses = idadeMeses
        };
    }
}
