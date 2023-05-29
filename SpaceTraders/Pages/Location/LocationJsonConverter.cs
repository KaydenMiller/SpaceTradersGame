using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpaceTraders.Pages.Location;

public class LocationJsonConverter : JsonConverter<Core.Location>
{
    public override Core.Location Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // null checking should be here 
        return Core.Location.CreateFromWaypoint(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, Core.Location value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}