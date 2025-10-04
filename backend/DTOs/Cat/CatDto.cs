namespace CatControl.API.DTOs.Cat;

public class CatDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; }
    public string? Raca { get; set; }
    public string? Cor { get; set; }
    public string? Sexo { get; set; }
    public bool Castrado { get; set; }
    public decimal? Peso { get; set; }
    public string? NumeroMicrochip { get; set; }
    public string? FotoUrl { get; set; }
    public string? Observacoes { get; set; }
    public int? IdadeAnos { get; set; }
    public int? IdadeMeses { get; set; }
}
