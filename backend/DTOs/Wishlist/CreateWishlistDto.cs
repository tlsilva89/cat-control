using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Wishlist;

public class CreateWishlistDto
{
    public int? CatId { get; set; }
    
    [Required(ErrorMessage = "Nome do produto é obrigatório")]
    [MaxLength(200)]
    public string NomeProduto { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? Categoria { get; set; }
    
    public decimal? PrecoEstimado { get; set; }
    
    [MaxLength(20)]
    public string Prioridade { get; set; } = "Média";
    
    [MaxLength(500)]
    public string? LinkProduto { get; set; }
    
    [MaxLength(100)]
    public string? Loja { get; set; }
    
    public string? Observacoes { get; set; }
}
