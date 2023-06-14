using Blazor.Extensions.Canvas.Canvas2D;

namespace SpaceTraders.Pages.Map;

public static class UniverseMapExtensions
{
    public static async Task AddCircle(this Canvas2DContext context, int x, int y, int radius)
    {
        await context.ArcAsync(x, y, radius, 0, 180);
        await context.ClosePathAsync();
    }

    public static async Task AddSolarSystem(this Canvas2DContext context, int x, int y, string name)
    {
        await context.SetTextAlignAsync(TextAlign.Center);
        await context.FillTextAsync(name, x, y - 7);
        await context.AddCircle(x, y, 5);
        await context.ClosePathAsync();
    }
}