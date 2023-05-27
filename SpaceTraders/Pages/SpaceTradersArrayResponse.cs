using System.Text.Json.Serialization;

namespace SpaceTraders.Pages;

public class SpaceTradersArrayResponse<TData> 
    where TData: class
{
    [JsonPropertyName("data")] 
    public IEnumerable<TData> Data { get; set; } = default!;

    [JsonPropertyName("meta")] 
    public Metadata Meta { get; set; } = default!;

    public class Metadata
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
        
        [JsonPropertyName("page")]
        public int Page { get; set; }
       
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}

public class SpaceTradersObjectResponse<TData> where TData : class
{
    [JsonPropertyName("data")] 
    public TData Data { get; set; } = default!;
}