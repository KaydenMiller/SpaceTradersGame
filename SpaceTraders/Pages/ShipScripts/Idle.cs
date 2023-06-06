using MediatR;
using MudBlazor;
using SpaceTraders.Pages.Notification;

namespace SpaceTraders.Pages.ShipScripts;

public class Idle : IScript
{
    private readonly IMediator _mediator;
    // private readonly NotificationsService _notificationsService;

    public bool AlreadyNotified { get; set; } = false;
    public string Name { get; } = nameof(Idle);
    public bool Running { get; } = false;

    public Idle(IMediator mediator)
    {
        _mediator = mediator;
    } 
    
    public Task Run(Core.Ship ship)
    {
        if (AlreadyNotified) { return Task.CompletedTask; }
        _mediator.Publish(new SnackBarNotification(ship.Id.ToString(), "Has gone idle", Severity.Normal));
        AlreadyNotified = true;
        return Task.CompletedTask;
    }
}