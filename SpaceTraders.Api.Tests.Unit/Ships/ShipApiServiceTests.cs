using FluentAssertions;
using Flurl.Http.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SpaceTraders.Api.Ships;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Tests.Unit.Ships;

public class ShipApiServiceTests
{
    private Mock<ILogger<ShipApiService>> _logger = new();
    private Mock<IConfiguration> _config = new();
   
    [Fact]
    public async Task GivenWeAreInAShipWithACooldown_WhenWeAttemptToExtract_ThenWeShouldHandleTheCooldownErrorCode()
    {
        var sut = new ShipApiService(new SpaceTradersApi(_config.Object), _logger.Object);

        var expirationTime = (DateTime.UtcNow + TimeSpan.FromSeconds(25));

        using var httpTest = new HttpTest();
        httpTest
           .ForCallsToSpaceTradersApi()
           .WithVerb(HttpMethod.Post)
           .RespondWithJson(new
           {
               error = new SpaceTradersError<ShipCooldown>()
               {
                   Message = "Ship is currently on cooldown.",
                   ErrorCode = ErrorCodes.COOLDOWN_CONFLICT_ERROR,
                   Data = new ShipCooldown()
                   {
                       ShipId = "ab-cdefg-a1dfe",
                       TotalSeconds = 90,
                       Expiration = expirationTime,
                       RemainingSeconds = 25
                   }
               }
           }, StatusCodes.Status409Conflict);

        var action = async () =>
        {
            _ = await sut.ExtractOre(new Ship()
            {
                Id = "ab-cdefg-a1dfe"
            });
        };

        await action.Should().ThrowAsync<SpaceTradersApiException<ShipCooldown>>();
    }
}