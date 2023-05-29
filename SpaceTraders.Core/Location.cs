namespace SpaceTraders.Core;

public struct Location
{
    private readonly string? _system;
    private readonly string? _waypoint;

    public string Sector { get; }

    public readonly string? System => $"{Sector}-{_system}";

    public readonly string? Waypoint => $"{Sector}-{_system}-{_waypoint}";

    public Location(string sector)
    {
        Sector = sector;
    }

    public Location(string sector, string system)
    {
        Sector = sector;
        _system = system;
    }

    public Location(string sector, string system, string waypoint)
    {
        Sector = sector;
        _system = system;
        _waypoint = waypoint;
    }

    public static Location CreateFromWaypoint(string waypoint)
    {
        // TODO: validate waypoint string here
        var waypointList = waypoint.Split("-");
        return new Location(waypointList[0], waypointList[1], waypointList[2]);
    }

    public override string ToString()
    {
        if (Waypoint is not null)
        {
            return Waypoint;
        }

        if (System is not null)
        {
            return System;
        }

        return Sector;
    }
}