using MudBlazor;

namespace SpaceTraders.Pages.ConditionalIcons;

/**
 * Title: If you are using an icon then it needs an on hover title 
 */
public record Icon(string Name, bool Display, string Title, Color Color = Color.Default, Size Size = Size.Medium);