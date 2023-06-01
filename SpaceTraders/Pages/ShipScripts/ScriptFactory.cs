using System.Diagnostics;

namespace SpaceTraders.Pages.ShipScripts;

public class ScriptFactory
{
    private readonly IServiceProvider _provider;
    
    public ScriptFactory(IServiceProvider provider)
    {
        _provider = provider;
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
            nameof(Idle) => _provider.GetRequiredService<Idle>(),
            nameof(MineAndSell) => _provider.GetRequiredService<MineAndSell>(),
            nameof(SurveyBelt) => _provider.GetRequiredService<SurveyBelt>(),
            nameof(AdvancedMineAndSell) => _provider.GetRequiredService<AdvancedMineAndSell>(),
            _ => throw new UnreachableException()
        };
    }
}