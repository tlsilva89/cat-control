using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CatControl.API.DTOs.Notification;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;
    
    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? "0");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserNotifications([FromQuery] bool? lida = null)
    {
        var userId = GetUserId();
        var notifications = await _notificationService.GetUserNotifications(userId, lida);
        return Ok(notifications);
    }
    
    [HttpGet("unread")]
    public async Task<IActionResult> GetUnreadNotifications()
    {
        var userId = GetUserId();
        var notifications = await _notificationService.GetUnreadNotifications(userId);
        return Ok(notifications);
    }
    
    [HttpGet("unread/count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var userId = GetUserId();
        var count = await _notificationService.GetUnreadCount(userId);
        return Ok(new { count });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id)
    {
        var userId = GetUserId();
        var notification = await _notificationService.GetNotificationById(id, userId);
        
        if (notification == null)
        {
            return NotFound(new { message = "Notificação não encontrada" });
        }
        
        return Ok(notification);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto createNotificationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userId = GetUserId();
        var notification = await _notificationService.CreateNotification(createNotificationDto, userId);
        
        if (notification == null)
        {
            return BadRequest(new { message = "Erro ao criar notificação" });
        }
        
        return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
    }
    
    [HttpPost("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var userId = GetUserId();
        var result = await _notificationService.MarkAsRead(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Notificação não encontrada" });
        }
        
        return Ok(new { message = "Notificação marcada como lida" });
    }
    
    [HttpPost("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var userId = GetUserId();
        await _notificationService.MarkAllAsRead(userId);
        
        return Ok(new { message = "Todas as notificações marcadas como lidas" });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var userId = GetUserId();
        var result = await _notificationService.DeleteNotification(id, userId);
        
        if (!result)
        {
            return NotFound(new { message = "Notificação não encontrada" });
        }
        
        return NoContent();
    }
}
