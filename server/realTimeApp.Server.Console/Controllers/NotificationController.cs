using Microsoft.AspNetCore.Mvc;
using realTimeApp.Server.Application;
using realTimeApp.Server.Application.Interfaces;
using realTimeApp.Server.Domain.Data.Entities;

namespace realTimeApp.Server.Console;
[ApiController]
[Route("api/[controller]/[action]")]
public class NotificationController : ControllerBase
{

    private readonly ISignalSenderService _senderService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(ILogger<NotificationController> logger, ISignalSenderService signalSenderService, INotificationService notificationService){
        _senderService = signalSenderService;
        _notificationService = notificationService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage([FromQuery] Notification notification){
        _logger.LogInformation("Starting PostMessage");
        await _notificationService.SendNotificationAsync(notification);

        return Ok();
    }
}
