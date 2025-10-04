using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class WeightHistory
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CatId { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Peso { get; set; }
    
    [Required]
    public DateTime DataPesagem { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CatId")]
    public Cat Cat { get; set; } = null!;
}
