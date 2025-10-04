using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatControl.API.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    public int? CatId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Tipo { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;
    
    [Required]
    public string Mensagem { get; set; } = string.Empty;
    
    public DateTime DataNotificacao { get; set; }
    
    public bool Lida { get; set; } = false;
    
    [MaxLength(20)]
    public string Prioridade { get; set; } = "Normal";
    
    public int? ReferenciaId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    [ForeignKey("CatId")]
    public Cat? Cat { get; set; }
}
