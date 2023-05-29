using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl.Http;
using Flurl.Http.Configuration;
using MudBlazor.Services;
using SpaceTraders.Pages;
using SpaceTraders.Pages.Contracts;
using SpaceTraders.Pages.Location;
using SpaceTraders.Pages.Player;
using SpaceTraders.Pages.Ship;
using SpaceTraders.Pages.ShipYard;

var builder = WebApplication.CreateBuilder(args);

FlurlHttp.Configure(settings =>
{
    var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
    options.Converters.Add(new JsonStringEnumConverter());
    settings.JsonSerializer = new DefaultJsonSerializer(options);
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<SpaceTradersApi>();
builder.Services.AddSingleton<ContractApiService>();
builder.Services.AddSingleton<PlayerApiService>();
builder.Services.AddSingleton<LocationApiService>();
builder.Services.AddSingleton<ShipYardApiService>();
builder.Services.AddSingleton<ShipApiService>();

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