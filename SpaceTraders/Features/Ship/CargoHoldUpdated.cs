using MediatR;
using MudBlazor;
using SpaceTraders.Core;
using SpaceTraders.Pages.Notification;

namespace SpaceTraders.Features.Ship;

public class CargoHoldUpdated : INotification
{
    public required string ShipId { get; set; }
    public required Location Waypoint { get; set; }
    public required int Capacity { get; set; }
    public required int TotalUnits { get; set; }
    
    public Core.Cargo? ShipCargo { get; set; }

    public static CargoHoldUpdated FromShipCargo(string shipId, Location shipLocation, Core.Cargo cargo)
    {
        return new CargoHoldUpdated()
        {
            Capacity = cargo.Capacity,
            Waypoint = shipLocation,
            ShipId = shipId,
            TotalUnits = cargo.TotalUnits,
            ShipCargo = cargo
        };
    }
}

public class CargoHoldUpdatedNotificationHandler : INotificationHandler<CargoHoldUpdated>
{
    private readonly IMediator _mediator;

    public CargoHoldUpdatedNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(CargoHoldUpdated notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(
            new SnackBarNotification(
                "Cargo Updated", 
                $"Cargo updated for ship {notification.ShipId}: {notification.TotalUnits}/{notification.Capacity}",
                Severity.Info));
    }
}