using MediatR;

namespace SpaceTraders.Features.Ship;

public class ShipUpdated : INotification
{
    public required Core.Ship Ship { get; set; }
}