using System.Diagnostics;
using SpaceTraders.Core;
using SpaceTraders.Pages.Ship;

namespace SpaceTraders.Pages.ShipScripts;

public class ScriptFactory
{
    private readonly ShipApiService _apiService;
    private readonly LoggerFactory _loggerFactory;

    public ScriptFactory(ShipApiService apiService, LoggerFactory loggerFactory)
    {
        _apiService = apiService;
        _loggerFactory = loggerFactory;
    }
    
    public IScript Create<TType>() where TType : IScript
    {
        var typeName = nameof(TType);
        return Create(typeName);
    }

    public IScript Create(string typeName)
    {
        return typeName switch
        {
            nameof(Idle) => new Idle(),
            nameof(MineAndSell) => new MineAndSell(_apiService, _loggerFactory.CreateLogger<MineAndSell>()),
            nameof(SurveyBelt) => new SurveyBelt(_apiService, _loggerFactory.CreateLogger<SurveyBelt>()),
            _ => throw new UnreachableException()
        };
    }
}