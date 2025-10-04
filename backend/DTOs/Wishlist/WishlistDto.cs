namespace CatControl.API.DTOs.Wishlist;

public class WishlistDto
{
    public int Id { get; set; }
    public int? CatId { get; set; }
    public string? CatNome { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public decimal? PrecoEstimado { get; set; }
    public string Prioridade { get; set; } = "Média";
    public string? LinkProduto { get; set; }
    public string? Loja { get; set; }
    public bool Comprado { get; set; }
    public DateTime? DataCompra { get; set; }
    public string? Observacoes { get; set; }
}
