using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceTraders.Game.Pages;

public class SpaceTradersResponse<TData> 
    where TData: class
{
    [JsonPropertyName("data")] 
    public IEnumerable<TData> Data { get; set; } = default!;
}