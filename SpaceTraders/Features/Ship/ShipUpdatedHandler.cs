using MediatR;
using SpaceTraders.Features.Cargo;

namespace SpaceTraders.Features.Ship;

public class ShipUpdatedHandler : INotificationHandler<ShipUpdated>
{
    private readonly ShipService _shipService;
    private readonly ILogger<ShipUpdatedHandler> _logger;

    public ShipUpdatedHandler(ShipService shipService, ILogger<ShipUpdatedHandler> logger)
    {
        _shipService = shipService;
        _logger = logger;
    }
    
    public Task Handle(ShipUpdated notification, CancellationToken cancellationToken)
    {
        _shipService.AddShip(notification.Ship);
    }
}