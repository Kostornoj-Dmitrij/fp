using System.Drawing;

namespace TagsCloudVisualization.Properties;

public class TagLayouterProperties
{
    public int MinSize { get; }
    public int MaxSize { get; }
    public FontFamily FontFamily { get; }

    public TagLayouterProperties(int minSize, int maxSize, string fontName)
    {
        MinSize = minSize;
        MaxSize = maxSize;
        FontFamily = new FontFamily(fontName);
    }
}