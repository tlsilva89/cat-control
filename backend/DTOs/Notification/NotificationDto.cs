namespace CatControl.API.DTOs.Notification;

public class NotificationDto
{
    public int Id { get; set; }
    public int? CatId { get; set; }
    public string? CatNome { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public DateTime DataNotificacao { get; set; }
    public bool Lida { get; set; }
    public string Prioridade { get; set; } = "Normal";
    public int? ReferenciaId { get; set; }
    public DateTime CreatedAt { get; set; }
}
