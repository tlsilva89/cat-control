using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Vaccine;

public class CreateVaccineDto
{
    [Required(ErrorMessage = "CatId é obrigatório")]
    public int CatId { get; set; }
    
    [Required(ErrorMessage = "Tipo de vacina é obrigatório")]
    [MaxLength(100)]
    public string TipoVacina { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Data de aplicação é obrigatória")]
    public DateTime DataAplicacao { get; set; }
    
    public DateTime? ProximaAplicacao { get; set; }
    
    [MaxLength(100)]
    public string? LocalAplicacao { get; set; }
    
    [MaxLength(100)]
    public string? Veterinario { get; set; }
    
    public decimal? Valor { get; set; }
    
    public string? Observacoes { get; set; }
}
