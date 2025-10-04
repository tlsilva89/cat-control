using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class VeterinaryVisit
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CatId { get; set; }
    
    [Required]
    public DateTime DataConsulta { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Veterinario { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Clinica { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string MotivoConsulta { get; set; } = string.Empty;
    
    public string? Diagnostico { get; set; }
    
    public string? Tratamento { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Valor { get; set; }
    
    public DateTime? ProximaConsulta { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CatId")]
    public Cat Cat { get; set; } = null!;
}
