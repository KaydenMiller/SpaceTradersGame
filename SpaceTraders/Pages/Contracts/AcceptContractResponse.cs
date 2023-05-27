using System.Text.Json.Serialization;

namespace SpaceTraders.Pages.Contracts;

public class AcceptContractResponse
{
    [JsonPropertyName("agent")]
    public Player.Player Player { get; set; }

    [JsonPropertyName("contract")]
    public Contract Contract { get; set; }
}