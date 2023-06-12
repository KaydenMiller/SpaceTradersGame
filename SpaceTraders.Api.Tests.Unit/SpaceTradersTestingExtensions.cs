using System.Runtime.CompilerServices;
using Flurl.Http.Testing;

namespace SpaceTraders.Api.Tests.Unit;

public static class SpaceTradersTestingExtensions
{
    public static FilteredHttpTestSetup ForCallsToSpaceTradersApi(this HttpTest httpTest)
    {
        return httpTest
           .ForCallsTo("*api.spacetraders.io/v2/*");
    }
}