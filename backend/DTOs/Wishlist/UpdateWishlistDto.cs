using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Wishlist;

public class UpdateWishlistDto
{
    public int? CatId { get; set; }
    
    [MaxLength(200)]
    public string? NomeProduto { get; set; }
    
    [MaxLength(50)]
    public string? Categoria { get; set; }
    
    public decimal? PrecoEstimado { get; set; }
    
    [MaxLength(20)]
    public string? Prioridade { get; set; }
    
    [MaxLength(500)]
    public string? LinkProduto { get; set; }
    
    [MaxLength(100)]
    public string? Loja { get; set; }
    
    public bool? Comprado { get; set; }
    
    public DateTime? DataCompra { get; set; }
    
    public string? Observacoes { get; set; }
}
