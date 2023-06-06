namespace SpaceTraders.Features.Cargo;

public class ShipService
{
    public IDictionary<string, Core.Ship> Ships { get; set; } = new Dictionary<string, Core.Ship>();

    public void AddShip(Core.Ship ship)
    {
        Ships.Add(ship.Id, ship);
    }

    public void RemoveShip(string shipId)
    {
        Ships.Remove(shipId);
    }

    public Core.Ship? GetShipById(string shipId)
    {
        if (Ships.TryGetValue(shipId, out var ship))
        {
            return ship;
        }
        return null;
    }
}