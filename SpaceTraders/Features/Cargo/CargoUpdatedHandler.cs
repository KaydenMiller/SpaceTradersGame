using MediatR;

namespace SpaceTraders.Features.Cargo;

public class CargoUpdatedHandler : INotificationHandler<CargoUpdated>
{
    private readonly ShipService _shipService;
    private readonly ILogger<CargoUpdatedHandler> _logger;

    public CargoUpdatedHandler(ShipService shipService, ILogger<CargoUpdatedHandler> logger)
    {
        _shipService = shipService;
        _logger = logger;
    }
    
    public Task Handle(CargoUpdated notification, CancellationToken cancellationToken)
    {
        var shipToUpdate = _shipService.GetShipById(notification.ShipId);

        if (shipToUpdate is null)
            _logger.LogError("Attempted to update a ship that does not exist");
        else 
            shipToUpdate.Cargo = notification.Cargo;
        
        return Task.CompletedTask;
    }
}