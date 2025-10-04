namespace CatControl.API.DTOs.Finance;

public class FinanceDto
{
    public int Id { get; set; }
    public int? CatId { get; set; }
    public string? CatNome { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataGasto { get; set; }
    public string? FormaPagamento { get; set; }
    public bool Recorrente { get; set; }
    public string? FrequenciaRecorrencia { get; set; }
    public string? Observacoes { get; set; }
}
