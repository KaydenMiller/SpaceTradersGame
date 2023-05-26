using System.Text.Json.Serialization;

namespace SpaceTraders.Pages;

public class SpaceTradersArrayResponse<TData> 
    where TData: class
{
    [JsonPropertyName("data")] 
    public IEnumerable<TData> Data { get; set; } = default!;
}

public class SpaceTradersObjectResponse<TData> where TData : class
{
    [JsonPropertyName("data")] 
    public TData Data { get; set; } = default!;
}