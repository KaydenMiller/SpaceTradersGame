using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class NavigationInfo
{
    [JsonPropertyName("systemSymbol")]
    public string SystemSymbol { get; set; }

    [JsonPropertyName("waypointSymbol")]
    public string WaypointSymbol { get; set; }

    [JsonPropertyName("route")]
    public NavigationRoute Route { get; set; }

    public class NavigationRoute
    {
        [JsonPropertyName("destination")]
        public NavigationEndpoint Endpoint { get; set; }

        [JsonPropertyName("departure")]
        public NavigationEndpoint Departure { get; set; }

        [JsonPropertyName("departureTime")]
        public DateTime DepartureTime { get; set; }

        [JsonPropertyName("arrival")]
        public DateTime Arrival { get; set; }

        public class NavigationEndpoint
        {
            [JsonPropertyName("symbol")]
            public string Symbol { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("x")]
            public int XCoordinate { get; set; }

            [JsonPropertyName("y")]
            public int YCoordinate { get; set; }
        }
    }
}