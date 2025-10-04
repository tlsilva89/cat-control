using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Vaccine;

public class UpdateVaccineDto
{
    [MaxLength(100)]
    public string? TipoVacina { get; set; }
    
    public DateTime? DataAplicacao { get; set; }
    
    public DateTime? ProximaAplicacao { get; set; }
    
    [MaxLength(100)]
    public string? LocalAplicacao { get; set; }
    
    [MaxLength(100)]
    public string? Veterinario { get; set; }
    
    public decimal? Valor { get; set; }
    
    public string? Observacoes { get; set; }
}
