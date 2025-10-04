using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Wishlist
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    public int? CatId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string NomeProduto { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? Categoria { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? PrecoEstimado { get; set; }
    
    [MaxLength(20)]
    public string Prioridade { get; set; } = "Média";
    
    [MaxLength(500)]
    public string? LinkProduto { get; set; }
    
    [MaxLength(100)]
    public string? Loja { get; set; }
    
    public bool Comprado { get; set; } = false;
    
    public DateTime? DataCompra { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    [ForeignKey("CatId")]
    public Cat? Cat { get; set; }
}
