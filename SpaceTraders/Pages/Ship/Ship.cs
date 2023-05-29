namespace SpaceTraders.Pages.Ship;

public record ShipId(string Value);

public class Ship
{
    public ShipId Id { get; }

    public Ship(ShipId id)
    {
        Id = id;
    }
}