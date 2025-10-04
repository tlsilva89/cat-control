using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Notification;
using CatControl.API.Models;

namespace CatControl.API.Services;

public class NotificationService : INotificationService
{
    private readonly AppDbContext _context;
    
    public NotificationService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<NotificationDto>> GetUserNotifications(int userId, bool? lida = null)
    {
        var query = _context.Notifications
            .Include(n => n.Cat)
            .Where(n => n.UserId == userId);
        
        if (lida.HasValue)
        {
            query = query.Where(n => n.Lida == lida.Value);
        }
        
        var notifications = await query
            .OrderByDescending(n => n.Prioridade == "Alta")
            .ThenByDescending(n => n.DataNotificacao)
            .ToListAsync();
        
        return notifications.Select(n => MapToNotificationDto(n));
    }
    
    public async Task<IEnumerable<NotificationDto>> GetUnreadNotifications(int userId)
    {
        var notifications = await _context.Notifications
            .Include(n => n.Cat)
            .Where(n => n.UserId == userId && !n.Lida)
            .OrderByDescending(n => n.Prioridade == "Alta")
            .ThenByDescending(n => n.DataNotificacao)
            .ToListAsync();
        
        return notifications.Select(n => MapToNotificationDto(n));
    }
    
    public async Task<NotificationDto?> GetNotificationById(int notificationId, int userId)
    {
        var notification = await _context.Notifications
            .Include(n => n.Cat)
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        
        if (notification == null) return null;
        
        return MapToNotificationDto(notification);
    }
    
    public async Task<NotificationDto?> CreateNotification(CreateNotificationDto createNotificationDto, int userId)
    {
        // Se CatId foi informado, verificar se pertence ao usuário
        if (createNotificationDto.CatId.HasValue)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(c => c.Id == createNotificationDto.CatId.Value && c.UserId == userId);
            if (cat == null) return null;
        }
        
        var notification = new Notification
        {
            UserId = userId,
            CatId = createNotificationDto.CatId,
            Tipo = createNotificationDto.Tipo,
            Titulo = createNotificationDto.Titulo,
            Mensagem = createNotificationDto.Mensagem,
            DataNotificacao = createNotificationDto.DataNotificacao,
            Prioridade = createNotificationDto.Prioridade,
            ReferenciaId = createNotificationDto.ReferenciaId,
            Lida = false,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        
        // Recarregar com Cat
        await _context.Entry(notification).Reference(n => n.Cat).LoadAsync();
        
        return MapToNotificationDto(notification);
    }
    
    public async Task<bool> MarkAsRead(int notificationId, int userId)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        
        if (notification == null) return false;
        
        notification.Lida = true;
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> MarkAllAsRead(int userId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId && !n.Lida)
            .ToListAsync();
        
        foreach (var notification in notifications)
        {
            notification.Lida = true;
        }
        
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> DeleteNotification(int notificationId, int userId)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        
        if (notification == null) return false;
        
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<int> GetUnreadCount(int userId)
    {
        return await _context.Notifications
            .CountAsync(n => n.UserId == userId && !n.Lida);
    }
    
    public async Task CreateVaccineNotification(int vaccineId, int userId)
    {
        var vaccine = await _context.Vaccines
            .Include(v => v.Cat)
            .FirstOrDefaultAsync(v => v.Id == vaccineId && v.Cat.UserId == userId);
        
        if (vaccine == null || !vaccine.ProximaAplicacao.HasValue) return;
        
        // Criar notificação 7 dias antes
        var dataNotificacao = vaccine.ProximaAplicacao.Value.AddDays(-7);
        
        var notification = new Notification
        {
            UserId = userId,
            CatId = vaccine.CatId,
            Tipo = "Vacina",
            Titulo = $"Vacina {vaccine.TipoVacina} próxima",
            Mensagem = $"A vacina {vaccine.TipoVacina} do {vaccine.Cat.Nome} vence em 7 dias ({vaccine.ProximaAplicacao.Value:dd/MM/yyyy})",
            DataNotificacao = dataNotificacao,
            Prioridade = "Alta",
            ReferenciaId = vaccineId,
            Lida = false,
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }
    
    public async Task CreateStockNotification(int stockId, int userId)
    {
        var stock = await _context.Stocks
            .FirstOrDefaultAsync(s => s.Id == stockId && s.UserId == userId);
        
        if (stock == null) return;
        
        // Criar notificação se estoque baixo
        if (stock.QuantidadeAtual <= stock.QuantidadeMinima)
        {
            var notification = new Notification
            {
                UserId = userId,
                Tipo = "Estoque",
                Titulo = "Estoque baixo",
                Mensagem = $"O estoque de {stock.NomeProduto} está baixo ({stock.QuantidadeAtual} {stock.Unidade})",
                DataNotificacao = DateTime.UtcNow,
                Prioridade = "Normal",
                ReferenciaId = stockId,
                Lida = false,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
    
    private NotificationDto MapToNotificationDto(Notification notification)
    {
        return new NotificationDto
        {
            Id = notification.Id,
            CatId = notification.CatId,
            CatNome = notification.Cat?.Nome,
            Tipo = notification.Tipo,
            Titulo = notification.Titulo,
            Mensagem = notification.Mensagem,
            DataNotificacao = notification.DataNotificacao,
            Lida = notification.Lida,
            Prioridade = notification.Prioridade,
            ReferenciaId = notification.ReferenciaId,
            CreatedAt = notification.CreatedAt
        };
    }
}
