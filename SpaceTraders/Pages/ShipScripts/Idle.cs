namespace SpaceTraders.Pages.ShipScripts;

public class Idle : IScript
{
    public string Name { get; } = "Idle";
    public bool Running { get; } = false;
    public Task Run(Core.Ship ship)
    {
        return Task.CompletedTask;
    }
}