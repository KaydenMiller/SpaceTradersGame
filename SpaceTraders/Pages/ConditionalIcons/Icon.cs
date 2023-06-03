using MudBlazor;

namespace SpaceTraders.Pages.ConditionalIcons;

public record Icon(string Name, bool Display, Color Color = Color.Default, Size Size = Size.Medium);