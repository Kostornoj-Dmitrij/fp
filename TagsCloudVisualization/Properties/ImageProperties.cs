using System.Drawing;

namespace TagsCloudVisualization.Properties;

public class ImageProperties
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Color BackgroundColor { get; }

    public ImageProperties(int width, int height, string colorName)
    {
        Width = width;
        Height = height;
        BackgroundColor = Color.FromName(colorName);
    }
}