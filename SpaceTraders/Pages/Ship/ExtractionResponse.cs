﻿using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class ExtractionResponse
{
    [JsonPropertyName("cooldown")]
    public ShipCooldown Cooldown { get; set; }

    [JsonPropertyName("extraction")]
    public Extraction Extraction { get; set; }

    [JsonPropertyName("cargo")]
    public Cargo Cargo { get; set; }
}