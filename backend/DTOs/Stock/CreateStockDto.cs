using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Stock;

public class CreateStockDto
{
    [Required(ErrorMessage = "Nome do produto é obrigatório")]
    [MaxLength(100)]
    public string NomeProduto { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Categoria é obrigatória")]
    [MaxLength(50)]
    public string Categoria { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Quantidade atual é obrigatória")]
    public int QuantidadeAtual { get; set; }
    
    [Required(ErrorMessage = "Quantidade mínima é obrigatória")]
    public int QuantidadeMinima { get; set; }
    
    [MaxLength(20)]
    public string? Unidade { get; set; }
    
    public DateTime? DataValidade { get; set; }
    
    public decimal? PrecoUnitario { get; set; }
    
    public decimal? ConsumoMedioDiario { get; set; }
    
    [MaxLength(100)]
    public string? Marca { get; set; }
    
    public string? Observacoes { get; set; }
}
