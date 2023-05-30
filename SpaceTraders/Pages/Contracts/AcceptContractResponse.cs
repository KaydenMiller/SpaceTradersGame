﻿using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Contracts;

public class AcceptContractResponse
{
    [JsonPropertyName("agent")]
    public Player.Player Player { get; set; }

    [JsonPropertyName("contract")]
    public Contract Contract { get; set; }
}