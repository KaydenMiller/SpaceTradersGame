using MediatR;
using SpaceTraders.Pages.Notification;

namespace SpaceTraders.Pages.ShipScripts;

public class Idle : IScript
{
    private readonly IMediator _mediator;
    // private readonly NotificationsService _notificationsService;
    
    
    public string Name { get; } = nameof(Idle);
    public bool Running { get; } = false;

    public Idle(IMediator mediator)
    {
        _mediator = mediator;
    } 
    
    public Task Run(Core.Ship ship)
    {
        _mediator.Publish(new SnackBarNotification());
        return Task.CompletedTask;
    }
}