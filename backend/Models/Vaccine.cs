using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Vaccine
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CatId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string TipoVacina { get; set; } = string.Empty;
    
    [Required]
    public DateTime DataAplicacao { get; set; }
    
    public DateTime? ProximaAplicacao { get; set; }
    
    [MaxLength(100)]
    public string? LocalAplicacao { get; set; }
    
    [MaxLength(100)]
    public string? Veterinario { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Valor { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CatId")]
    public Cat Cat { get; set; } = null!;
}
