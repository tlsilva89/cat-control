using System.ComponentModel.DataAnnotations;

namespace CatControl.API.DTOs.Notification;

public class CreateNotificationDto
{
    public int? CatId { get; set; }
    
    [Required(ErrorMessage = "Tipo é obrigatório")]
    [MaxLength(50)]
    public string Tipo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Título é obrigatório")]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Mensagem é obrigatória")]
    public string Mensagem { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Data da notificação é obrigatória")]
    public DateTime DataNotificacao { get; set; }
    
    [MaxLength(20)]
    public string Prioridade { get; set; } = "Normal";
    
    public int? ReferenciaId { get; set; }
}
