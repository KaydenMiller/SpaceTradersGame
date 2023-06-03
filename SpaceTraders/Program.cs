using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl.Http;
using Flurl.Http.Configuration;
using MudBlazor;
using MudBlazor.Services;
using SpaceTraders.Pages;
using SpaceTraders.Pages.Contracts;
using SpaceTraders.Pages.Location;
using SpaceTraders.Pages.Market;
using SpaceTraders.Pages.Player;
using SpaceTraders.Pages.Ship;
using SpaceTraders.Pages.ShipScripts;
using SpaceTraders.Pages.ShipYard;

var builder = WebApplication.CreateBuilder(args);

FlurlHttp.Configure(settings =>
{
    var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
    options.Converters.Add(new JsonStringEnumConverter());
    options.Converters.Add(new LocationJsonConverter());
    options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    settings.JsonSerializer = new DefaultJsonSerializer(options);

    settings.AfterCallAsync = async call =>
    {
        Console.WriteLine($"REQUEST: {call.Request.Verb.Method}: {call.Request.Url}");
        Console.WriteLine(call.RequestBody);
        Console.WriteLine($"RESPONSE: {call.Response.StatusCode} {await call.Response.ResponseMessage.Content.ReadAsStringAsync()}");
        foreach (var header in call.Response.Headers)
        {
            Console.WriteLine($"\t{header.Name}: {header.Value}");
        }
    };
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(cfg =>
{
    cfg.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    cfg.SnackbarConfiguration.PreventDuplicates = false;
    cfg.SnackbarConfiguration.ShowCloseIcon = true;
});;

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.Converters.Add(new LocationJsonConverter());
    options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddSingleton<LoggerFactory>();
builder.Services.AddSingleton<SpaceTradersApi>();
builder.Services.AddSingleton<ContractApiService>();
builder.Services.AddSingleton<PlayerApiService>();
builder.Services.AddSingleton<LocationApiService>();
builder.Services.AddSingleton<ShipYardApiService>();
builder.Services.AddSingleton<ShipApiService>();
builder.Services.AddSingleton<MarketApiService>();

builder.Services.AddSingleton<ScriptFactory>();
builder.Services.AddSingleton<ShipScriptService>();

builder.Services.AddTransient<Idle>();
builder.Services.AddTransient<MineAndSell>();
builder.Services.AddTransient<SurveyBelt>();
builder.Services.AddTransient<AdvancedMineAndSell>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();