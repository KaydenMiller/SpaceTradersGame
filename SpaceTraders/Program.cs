using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl.Http;
using Flurl.Http.Configuration;
using MudBlazor;
using MudBlazor.Services;
using Serilog;
using SpaceTraders.Api;
using SpaceTraders.Core;
using SpaceTraders.Features.Cargo;
using SpaceTraders.Pages.Map;
using SpaceTraders.Pages.ShipScripts;

Log.Logger = new LoggerConfiguration()
   .Enrich.FromLogContext()
   .WriteTo.Console()
   .WriteTo.File(
        "./logs/",
        rollingInterval: RollingInterval.Hour,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 2,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(1)
    )
   .WriteTo.Seq("http://localhost:5341")
   .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    FlurlHttp.Configure(settings =>
    {
        var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.Add(new LocationJsonConverter());
        options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        settings.JsonSerializer = new DefaultJsonSerializer(options);

        settings.AfterCallAsync = async call =>
        {
            Log.Debug("HTTP {Verb} {Url} responded {StatusCode} with body {Body} containing headers {@Headers}",
                call.Request.Verb.Method,
                call.Request.Url,
                call.Response.StatusCode,
                await call.Response.ResponseMessage.Content.ReadAsStringAsync(),
                call.Response.Headers.Select(x => new
                {
                    x.Name, x.Value
                }));
        };
    });

    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddMudServices(cfg =>
    {
        cfg.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
        cfg.SnackbarConfiguration.PreventDuplicates = false;
        cfg.SnackbarConfiguration.ShowCloseIcon = true;
    });

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.SerializerOptions.Converters.Add(new LocationJsonConverter());
        options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    });

    builder.Services.AddSpaceTradersApi();
    builder.Services.AddSingleton<UniverseMapGenerator>();

    builder.Services.AddSingleton<LoggerFactory>();

    builder.Services.AddSingleton<ShipService>();

    builder.Services.AddSingleton<ScriptFactory>();
    builder.Services.AddSingleton<ShipScriptService>();

    builder.Services.AddTransient<Idle>();
    builder.Services.AddTransient<MineAndSell>();
    builder.Services.AddTransient<SurveyBelt>();
    builder.Services.AddTransient<AdvancedMineAndSell>();

    builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<Program>(); });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly!");
}
finally
{
    Log.CloseAndFlush();
}