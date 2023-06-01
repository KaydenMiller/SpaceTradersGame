namespace SpaceTraders.Pages.ShipScripts;

public class ShipScriptService
{
    private readonly ILogger<ShipScriptService> _logger;
    private readonly ScriptFactory _scriptFactory;
    private Dictionary<string, ScriptRunData> ScriptAssignments { get; set; } = new();

    private readonly System.Timers.Timer _scriptTimer = new();

    public ShipScriptService(ScriptFactory scriptFactory, ILogger<ShipScriptService> logger, LoggerFactory loggerFactory)
    {
        _logger = logger;
        _scriptFactory = scriptFactory;

        _scriptTimer.AutoReset = true;
        _scriptTimer.Interval = TimeSpan.FromSeconds(10).TotalMilliseconds;
        _scriptTimer.Elapsed += (_, _) =>
        {
            // Check if script is currently running or done
            foreach (var scriptAssignment in ScriptAssignments)
            {
                _logger.LogDebug("Checking if {ShipId} is running {ScriptName}, status is {Running}", scriptAssignment.Value.Ship.Id, scriptAssignment.Value.Script.Name, scriptAssignment.Value.Script.Running);
                if (scriptAssignment.Value.Script.Running is false)
                {
                    scriptAssignment.Value.Script.Run(scriptAssignment.Value.Ship);
                }
            }
        };
        _scriptTimer.Start();
    }

    public void RegisterShip(Core.Ship ship, string scriptName)
    {
        ScriptAssignments.Remove(ship.Id);
        ScriptAssignments.Add(ship.Id, new()
        {
            Script = _scriptFactory.Create(scriptName),
            Ship = ship
        });
    }

    public string GetActiveScript(Core.Ship ship)
    { 
        _logger.LogInformation("Getting Script for ship with id: {ShipId}", ship.Id);
        var scriptAssignment = ScriptAssignments.GetValueOrDefault(ship.Id);
        return scriptAssignment?.Script.Name ?? "Idle";
    }
}