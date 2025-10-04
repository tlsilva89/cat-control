using CatControl.API.DTOs.Notification;

namespace CatControl.API.Services;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetUserNotifications(int userId, bool? lida = null);
    Task<IEnumerable<NotificationDto>> GetUnreadNotifications(int userId);
    Task<NotificationDto?> GetNotificationById(int notificationId, int userId);
    Task<NotificationDto?> CreateNotification(CreateNotificationDto createNotificationDto, int userId);
    Task<bool> MarkAsRead(int notificationId, int userId);
    Task<bool> MarkAllAsRead(int userId);
    Task<bool> DeleteNotification(int notificationId, int userId);
    Task<int> GetUnreadCount(int userId);
}
