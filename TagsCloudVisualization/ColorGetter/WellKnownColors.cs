using System.Drawing;

namespace TagsCloudVisualization.ColorGetter;

public static class WellKnownColors
{
    public static readonly Dictionary<string, Color> Colors = new()
    {
        { "customRed", Color.FromArgb(200, 30, 30) },
        { "customGreen", Color.FromArgb(30, 200, 30) },
        { "customBlue", Color.FromArgb(30, 30, 200) },
        { "customYellow", Color.FromArgb(230, 230, 50) },
        { "customCyan", Color.FromArgb(50, 230, 230) },
        { "customMagenta", Color.FromArgb(230, 50, 230) },
        { "customBlack", Color.FromArgb(30, 30, 30) },
        { "customWhite", Color.FromArgb(250, 250, 250) },
        { "customGray", Color.FromArgb(100, 100, 100) },
        { "customOrange", Color.FromArgb(255, 140, 0) },
        { "customPurple", Color.FromArgb(150, 0, 150) },
        { "customBrown", Color.FromArgb(150, 75, 0) },
    };
}