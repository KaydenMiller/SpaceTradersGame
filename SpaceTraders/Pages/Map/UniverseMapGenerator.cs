namespace SpaceTraders.Pages.Map;

public class UniverseMapGenerator
{
    private IList<SolarSystem> _solarSystems = new List<SolarSystem>();
    private IList<Waypoint> _waypoints = new List<Waypoint>();

    public record Coordinates(int X, int Y);
    public record SolarSystem(string Name, Coordinates Coordinates);
    public record Waypoint(string Name, string Type, Coordinates Coordinates);

    public IEnumerable<SolarSystem> GetSolarSystems()
    {
        return _solarSystems;
    }

    public void AddSolarSystem(SolarSystem solarSystem)
    {
        _solarSystems.Add(solarSystem);
    }

    public IEnumerable<Waypoint> GetWaypoints()
    {
        return _waypoints;
    }

    public void AddWaypoint(Waypoint waypoint)
    {
        _waypoints.Add(waypoint);
    }
}