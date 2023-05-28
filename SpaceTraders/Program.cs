using MudBlazor.Services;
using SpaceTraders.Pages.Contracts;
using SpaceTraders.Pages.Location;
using SpaceTraders.Pages.Player;
using SpaceTraders.Pages.ShipYard;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<ContractApiService>();
builder.Services.AddSingleton<PlayerApiService>();
builder.Services.AddSingleton<LocationApiService>();
builder.Services.AddSingleton<ShipYardApiService>();

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