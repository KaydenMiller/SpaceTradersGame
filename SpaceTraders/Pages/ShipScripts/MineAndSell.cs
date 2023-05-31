using SpaceTraders.Pages.Ship;

namespace SpaceTraders.Pages.ShipScripts;

public class MineAndSell : IScript
{
    private readonly ShipApiService _shipApiService;
    private readonly ILogger<MineAndSell> _logger;

    public string Name { get; } = "MineAndSell";
    public bool Running { get; private set; } = false;

    public MineAndSell(ShipApiService shipApiService, ILogger<MineAndSell> logger)
    {
        _shipApiService = shipApiService;
        _logger = logger;
    }
    
    public async Task Run(Core.Ship ship)
    {
        Running = true; 
        
        // orbit
        _logger.LogInformation("Orbiting");
        await _shipApiService.OrbitCurrent(ship);
        await Task.Delay(TimeSpan.FromSeconds(5));

        // get cargo
        _logger.LogInformation("Getting Cargo");
        var cargoResponse = await _shipApiService.GetShipCargo(ship);
        await Task.Delay(TimeSpan.FromSeconds(5));       
        
        // check for existing cooldown
        _logger.LogInformation("Check Cooldown");
        var cooldown = await _shipApiService.GetShipCooldown(ship);
        var initialCooldown = (cooldown?.Expiration - DateTime.UtcNow) ?? TimeSpan.Zero;
        _logger.LogInformation("Cooldown Expiration: {Expiration}", cooldown?.Expiration.ToString());
        _logger.LogInformation("Cooldown Until Next Extraction: {Cooldown}", initialCooldown.ToString());
        await Task.Delay(initialCooldown);

        // extract
        while (cargoResponse.TotalUnits < cargoResponse.Capacity)
        {
            _logger.LogInformation("Extracting");
            var extractionResponse = await _shipApiService.ExtractOre(ship);
            cooldown = extractionResponse.Cooldown;
            var timeUntilReady = cooldown.Expiration - DateTime.UtcNow;
            _logger.LogInformation("Cooldown Expiration: {Expiration}", cooldown.Expiration.ToString());
            _logger.LogInformation("Cooldown Until Next Extraction: {Cooldown}", timeUntilReady.ToString());
            _logger.LogInformation("Cargo: {Count} of {Capacity}", extractionResponse.Cargo.TotalUnits, extractionResponse.Cargo.Capacity);
            cargoResponse = extractionResponse.Cargo;
            await Task.Delay(timeUntilReady ?? TimeSpan.FromSeconds(120));
        } 
    
        // dock 
        _logger.LogInformation("Docking");
        await _shipApiService.DockCurrent(ship);
        await Task.Delay(TimeSpan.FromSeconds(5));
    
        // get cargo
        _logger.LogInformation("Getting Cargo");
        var cargo = (await _shipApiService.GetShipCargo(ship)).InventoryItems;
        await Task.Delay(TimeSpan.FromSeconds(5));        
    
        // sell cargo
        foreach (var item in cargo)
        {
            _logger.LogInformation("Selling: {ItemId} at count of {Quantity}", item.Id, item.Quantity);
            await _shipApiService.SellCargo(ship, item.Id, item.Quantity);
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    
        _logger.LogInformation("Mine and Sell: Completed");
        Running = false;
    }
}