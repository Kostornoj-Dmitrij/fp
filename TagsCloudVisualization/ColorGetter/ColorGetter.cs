using System.Drawing;
using TagsCloudVisualization.Properties;

namespace TagsCloudVisualization.ColorGetter;

public class ColorGetter : IColorGetter
{
    private readonly ColorGetterProperties _colorGetterProperties;
    private readonly Random _random;

    public ColorGetter(ColorGetterProperties colorGetterProperties)
    {
        _colorGetterProperties = colorGetterProperties;
        _random = new Random();
    }

    public Color GetColor()
    {
        if (_colorGetterProperties.ColorName == "random")
        {
            return Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255));
        }

        if (WellKnownColors.Colors.TryGetValue(_colorGetterProperties.ColorName, out var customColor))
        {
            return customColor;
        }

        return Color.FromName(_colorGetterProperties.ColorName);
    }
}