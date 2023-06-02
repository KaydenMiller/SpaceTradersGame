using MediatR;

namespace SpaceTraders.Pages.Notification;

public class SnackBarNotification : INotification
{
    public string Name { get; set; }
    public string Description { get; set; }
    public MudBlazor.Severity Severity { get; set; }
    public MudBlazor.Color Color { get; set; }
    public MudBlazor.Variant Variant { get; set; }
    public SnackBarNotification()
    {
        
    }
}

public class SnackBarNotificationEventArgs : EventArgs
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required MudBlazor.Severity Severity { get; set; }
    public required MudBlazor.Color Color { get; set; }
    public required MudBlazor.Variant? Variant { get; set; }
}


public partial class NotificationProvider : INotificationHandler<SnackBarNotification>
{
    public static event EventHandler<SnackBarNotificationEventArgs>? InfoNotificationReceived;
    public Task Handle(SnackBarNotification notification, CancellationToken cancellationToken)
    {
        InfoNotificationReceived?.Invoke(this, new SnackBarNotificationEventArgs()
        {
            Name = notification.Name,
            Description = notification.Description,
            Severity = notification.Severity,
            Color = notification.Color,
            Variant = notification.Variant
        });
        return Task.CompletedTask;
    }
}