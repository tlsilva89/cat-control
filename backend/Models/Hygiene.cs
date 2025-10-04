using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Hygiene
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int CatId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string TipoCuidado { get; set; } = string.Empty;
    
    [Required]
    public DateTime DataRealizacao { get; set; }
    
    public DateTime? ProximoAgendamento { get; set; }
    
    public int? FrequenciaDias { get; set; }
    
    [MaxLength(200)]
    public string? ProdutoUtilizado { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CatId")]
    public Cat Cat { get; set; } = null!;
}
