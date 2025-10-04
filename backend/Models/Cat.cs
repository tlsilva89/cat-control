using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Cat
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    public DateTime? DataNascimento { get; set; }
    
    [MaxLength(50)]
    public string? Raca { get; set; }
    
    [MaxLength(50)]
    public string? Cor { get; set; }
    
    [MaxLength(10)]
    public string? Sexo { get; set; }
    
    public bool Castrado { get; set; } = false;
    
    [Column(TypeName = "decimal(5,2)")]
    public decimal? Peso { get; set; }
    
    [MaxLength(50)]
    public string? NumeroMicrochip { get; set; }
    
    [MaxLength(500)]
    public string? FotoUrl { get; set; }
    
    public string? Observacoes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    public ICollection<Vaccine> Vacinas { get; set; } = new List<Vaccine>();
    public ICollection<Deworming> Vermifugos { get; set; } = new List<Deworming>();
    public ICollection<Hygiene> Cuidados { get; set; } = new List<Hygiene>();
    public ICollection<WeightHistory> HistoricoPeso { get; set; } = new List<WeightHistory>();
    public ICollection<VeterinaryVisit> ConsultasVeterinarias { get; set; } = new List<VeterinaryVisit>();
}
