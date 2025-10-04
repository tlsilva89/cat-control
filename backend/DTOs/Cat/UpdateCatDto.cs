using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Cat;

public class UpdateCatDto
{
    [MaxLength(100)]
    public string? Nome { get; set; }
    
    public DateTime? DataNascimento { get; set; }
    
    [MaxLength(50)]
    public string? Raca { get; set; }
    
    [MaxLength(50)]
    public string? Cor { get; set; }
    
    [MaxLength(10)]
    public string? Sexo { get; set; }
    
    public bool? Castrado { get; set; }
    
    public decimal? Peso { get; set; }
    
    [MaxLength(50)]
    public string? NumeroMicrochip { get; set; }
    
    [MaxLength(500)]
    public string? FotoUrl { get; set; }
    
    public string? Observacoes { get; set; }
}
