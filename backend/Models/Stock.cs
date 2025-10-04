using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Stock
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string NomeProduto { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Categoria { get; set; } = string.Empty;
    
    [Required]
    public int QuantidadeAtual { get; set; }
    
    [Required]
    public int QuantidadeMinima { get; set; }
    
    [MaxLength(20)]
    public string? Unidade { get; set; }
    
    public DateTime? DataValidade { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? PrecoUnitario { get; set; }
    
    public decimal? ConsumoMedioDiario { get; set; }
    
    [MaxLength(100)]
    public string? Marca { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
