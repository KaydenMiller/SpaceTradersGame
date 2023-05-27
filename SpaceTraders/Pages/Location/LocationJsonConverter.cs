using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpaceTraders.Pages.Location;

public class LocationJsonConverter : JsonConverter<Location>
{
    public override Location Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // null checking should be here 
        return Location.CreateFromWaypoint(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, Location value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}