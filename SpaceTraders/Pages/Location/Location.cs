namespace SpaceTraders.Pages.Location;

public struct Location
{
    private readonly string _sector;
    private readonly string? _system;
    private readonly string? _waypoint;

    public readonly string Sector => _sector;

    public readonly string? System => $"{_sector}-{_system}";

    public readonly string? Waypoint => $"{_sector}-{_system}-{_waypoint}";

    public Location(string sector)
    {
        this._sector = sector;
    }

    public Location(string sector, string system)
    {
        this._sector = sector;
        this._system = system;
    }

    public Location(string sector, string system, string waypoint)
    {
        this._sector = sector;
        this._system = system;
        this._waypoint = waypoint;
    }

    static Location CreateFromWaypoint(string waypoint)
    {
        // TODO: validate waypoint string here
        var waypointList = waypoint.Split("-");
        return new Location(waypointList[0], waypointList[1], waypointList[2]);
    }
}