namespace SpaceTraders.Pages.Location;

public struct Waypoint
{
    private readonly string _sector;
    private readonly string? _system;
    private readonly string? _location;

    public readonly string Sector => _sector;

    public readonly string? System => $"{_sector}-{_system}";

    public readonly string? Location => $"{_sector}-{_system}-{_location}";

    public Waypoint(string sector)
    {
        this._sector = sector;
    }

    public Waypoint(string sector, string system)
    {
        this._sector = sector;
        this._system = system;
    }

    public Waypoint(string sector, string system, string location)
    {
        this._sector = sector;
        this._system = system;
        this._location = location;
    }

    static Waypoint CreateFromWaypoint(string waypoint)
    {
        // TODO: validate waypoint string here
        var waypointList = waypoint.Split("-");
        return new Waypoint(waypointList[0], waypointList[1], waypointList[2]);
    }
}