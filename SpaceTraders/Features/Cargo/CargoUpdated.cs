using MediatR;

namespace SpaceTraders.Features.Cargo;

public class CargoUpdated : INotification
{
    public required string ShipId { get; set; }
    public required Core.Cargo Cargo { get; set; }
}