using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Deworming
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CatId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Medicamento { get; set; } = string.Empty;
    
    [Required]
    public DateTime DataAplicacao { get; set; }
    
    public DateTime? ProximaAplicacao { get; set; }
    
    [Column(TypeName = "decimal(5,2)")]
    public decimal? Dosagem { get; set; }
    
    [Column(TypeName = "decimal(5,2)")]
    public decimal? PesoGatoNaData { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CatId")]
    public Cat Cat { get; set; } = null!;
}
