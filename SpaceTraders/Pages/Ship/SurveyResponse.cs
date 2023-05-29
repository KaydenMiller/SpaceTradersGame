using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class SurveyResponse
{
    [JsonPropertyName("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonPropertyName("surveys")]
    public IEnumerable<Survey> Surveys { get; set; }
}