using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Finance
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    public int? CatId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Categoria { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Valor { get; set; }
    
    [Required]
    public DateTime DataGasto { get; set; }
    
    [MaxLength(50)]
    public string? FormaPagamento { get; set; }
    
    public bool Recorrente { get; set; } = false;
    
    [MaxLength(20)]
    public string? FrequenciaRecorrencia { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    [ForeignKey("CatId")]
    public Cat? Cat { get; set; }
}
