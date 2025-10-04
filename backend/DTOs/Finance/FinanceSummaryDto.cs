namespace CatControl.API.DTOs.Finance;

public class FinanceSummaryDto
{
    public decimal TotalGasto { get; set; }
    public decimal GastoMensal { get; set; }
    public Dictionary<string, decimal> GastoPorCategoria { get; set; } = new();
    public List<FinanceDto> UltimosGastos { get; set; } = new();
}
