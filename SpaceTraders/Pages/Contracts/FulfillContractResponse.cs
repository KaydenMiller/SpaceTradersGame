using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Contracts;

public class FulfillContractResponse
{
    [JsonPropertyName("player")]
    public Player.Player Player { get; set; }

    [JsonPropertyName("contract")]
    public Contract Contract { get; set; }
}