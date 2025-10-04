namespace CatControl.API.DTOs.Stock;

public class StockDto
{
    public int Id { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int QuantidadeAtual { get; set; }
    public int QuantidadeMinima { get; set; }
    public string? Unidade { get; set; }
    public DateTime? DataValidade { get; set; }
    public decimal? PrecoUnitario { get; set; }
    public decimal? ConsumoMedioDiario { get; set; }
    public string? Marca { get; set; }
    public string? Observacoes { get; set; }
    public bool AlertaReposicao { get; set; }
    public int? DiasParaVencer { get; set; }
    public int? DiasEstimadosDuracao { get; set; }
}
