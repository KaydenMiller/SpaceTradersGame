using SpaceTraders.Pages.Notification;

namespace SpaceTraders.Pages.ShipScripts;

public class Idle : IScript
{
    private readonly NotificationsService _notificationsService;
    
    public string Name { get; } = nameof(Idle);
    public bool Running { get; } = false;

    public Idle(NotificationsService notificationsService)
    {
        _notificationsService = notificationsService;
    } 
    
    public Task Run(Core.Ship ship)
    {
        _notificationsService.InfoNotification("This is a test");
        return Task.CompletedTask;
    }
}