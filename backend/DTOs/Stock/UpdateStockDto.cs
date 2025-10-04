using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Stock;

public class UpdateStockDto
{
    [MaxLength(100)]
    public string? NomeProduto { get; set; }
    
    [MaxLength(50)]
    public string? Categoria { get; set; }
    
    public int? QuantidadeAtual { get; set; }
    
    public int? QuantidadeMinima { get; set; }
    
    [MaxLength(20)]
    public string? Unidade { get; set; }
    
    public DateTime? DataValidade { get; set; }
    
    public decimal? PrecoUnitario { get; set; }
    
    public decimal? ConsumoMedioDiario { get; set; }
    
    [MaxLength(100)]
    public string? Marca { get; set; }
    
    public string? Observacoes { get; set; }
}
