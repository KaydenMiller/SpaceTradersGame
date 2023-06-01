using SpaceTraders.Core;

namespace SpaceTraders.Pages.ShipScripts;

public static class ScriptExtensions
{
    public static TimeSpan? GetWaitTime(this ShipCooldown cooldown)
    {
        return cooldown.Expiration - DateTime.UtcNow;
    }
}