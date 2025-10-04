namespace CatControl.API.DTOs.Vaccine;

public class VaccineDto
{
    public int Id { get; set; }
    public int CatId { get; set; }
    public string CatNome { get; set; } = string.Empty;
    public string TipoVacina { get; set; } = string.Empty;
    public DateTime DataAplicacao { get; set; }
    public DateTime? ProximaAplicacao { get; set; }
    public string? LocalAplicacao { get; set; }
    public string? Veterinario { get; set; }
    public decimal? Valor { get; set; }
    public string? Observacoes { get; set; }
    public int? DiasParaProxima { get; set; }
}
