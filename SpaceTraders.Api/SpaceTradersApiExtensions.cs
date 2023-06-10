using Microsoft.Extensions.DependencyInjection;
using SpaceTraders.Api.Contracts;
using SpaceTraders.Api.Locations;
using SpaceTraders.Api.Markets;
using SpaceTraders.Api.Players;
using SpaceTraders.Api.Ships;
using SpaceTraders.Api.Shipyards;

namespace SpaceTraders.Api;

public static class SpaceTradersApiExtensions
{
    public static IServiceCollection AddSpaceTradersApi(this IServiceCollection services)
    {
        services.AddSingleton<SpaceTradersApi>();
        services.AddSingleton<ContractApiService>();
        services.AddSingleton<PlayerApiService>();
        services.AddSingleton<ShipApiService>();
        services.AddSingleton<MarketApiService>();
        services.AddSingleton<ShipYardApiService>();
        services.AddSingleton<LocationApiService>();
        
        return services;
    }
    
}