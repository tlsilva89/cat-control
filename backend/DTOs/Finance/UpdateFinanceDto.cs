using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Finance;

public class UpdateFinanceDto
{
    public int? CatId { get; set; }
    
    [MaxLength(200)]
    public string? Descricao { get; set; }
    
    [MaxLength(50)]
    public string? Categoria { get; set; }
    
    public decimal? Valor { get; set; }
    
    public DateTime? DataGasto { get; set; }
    
    [MaxLength(50)]
    public string? FormaPagamento { get; set; }
    
    public bool? Recorrente { get; set; }
    
    [MaxLength(20)]
    public string? FrequenciaRecorrencia { get; set; }
    
    public string? Observacoes { get; set; }
}
