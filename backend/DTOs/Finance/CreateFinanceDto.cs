using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Finance;

public class CreateFinanceDto
{
    public int? CatId { get; set; }
    
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [MaxLength(200)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Categoria é obrigatória")]
    [MaxLength(50)]
    public string Categoria { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Valor é obrigatório")]
    public decimal Valor { get; set; }
    
    [Required(ErrorMessage = "Data do gasto é obrigatória")]
    public DateTime DataGasto { get; set; }
    
    [MaxLength(50)]
    public string? FormaPagamento { get; set; }
    
    public bool Recorrente { get; set; } = false;
    
    [MaxLength(20)]
    public string? FrequenciaRecorrencia { get; set; }
    
    public string? Observacoes { get; set; }
}
