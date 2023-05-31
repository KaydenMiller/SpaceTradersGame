using SpaceTraders.Pages.Ship;

namespace SpaceTraders.Pages.ShipScripts;

public class SurveyBelt : IScript
{
    private readonly ShipApiService _shipApiService;
    private readonly ILogger<SurveyBelt> _logger;
    public string Name { get; } = nameof(SurveyBelt);
    public bool Running { get; private set; }

    public SurveyBelt(ShipApiService shipApiService, ILogger<SurveyBelt> logger)
    {
        _shipApiService = shipApiService;
        _logger = logger;
    }
    
    public async Task Run(Core.Ship ship)
    {
        Running = true;
        
        // Are we in orbit
        _logger.LogInformation("Checking if we are in orbit for survey");
        _ = await _shipApiService.OrbitCurrent(ship);
        await Task.Delay(TimeSpan.FromSeconds(5));
        
        _logger.LogInformation("Check if we have a cooldown");
        var cooldown = await _shipApiService.GetShipCooldown(ship);
        var cooldownWaitTime = (cooldown?.Expiration - DateTime.UtcNow) ?? TimeSpan.Zero;
        await Task.Delay(cooldownWaitTime);
        
        _logger.LogInformation("Survey the current location");
        var surveyResult = await _shipApiService.Survey(ship);
        _logger.LogInformation("Survey Information {SurveysCompleted}", surveyResult.Surveys.Count());

        Running = false;
    }
}