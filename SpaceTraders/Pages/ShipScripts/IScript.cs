namespace SpaceTraders.Pages.ShipScripts;

public interface IScript
{
    public string Name { get; }
    public bool Running { get; }
    public Task Run(Core.Ship ship);
}