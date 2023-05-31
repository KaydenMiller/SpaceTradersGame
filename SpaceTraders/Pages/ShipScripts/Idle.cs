namespace SpaceTraders.Pages.ShipScripts;

public class Idle : IScript
{
    public string Name { get; } = nameof(Idle);
    public bool Running { get; } = false;
    public Task Run(Core.Ship ship)
    {
        return Task.CompletedTask;
    }
}