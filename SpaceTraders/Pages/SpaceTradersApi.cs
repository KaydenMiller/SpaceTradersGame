namespace SpaceTraders.Pages;

public class SpaceTradersApi
{
    private readonly IConfiguration _configuration;
    public const string API_ROOT = "https://api.spacetraders.io/v2";

    public string ApiToken => _configuration["SpaceTraders:ApiKey"]!;
    public SpaceTradersApi(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}