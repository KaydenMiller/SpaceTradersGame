namespace SpaceTraders.Pages.Notification;

public delegate void NotificationHandler(string type, string name);

public class NotificationsService
{
    public event NotificationHandler? Notification;

    public NotificationsService(ILogger<NotificationsService> logger)
    {
        Notification += (_, _) =>
        {
            logger.LogInformation("Notification was triggered");
        };
    }

    public void InfoNotification(string name)
    {
        Notification?.Invoke("INFO", name);
    }
}