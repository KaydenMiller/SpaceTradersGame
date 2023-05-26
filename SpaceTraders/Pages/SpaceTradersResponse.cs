using System.Text.Json.Serialization;

namespace SpaceTraders.Pages;

public class SpaceTradersResponse<TData> 
    where TData: class
{
    [JsonPropertyName("data")] 
    public IEnumerable<TData> Data { get; set; } = default!;
}