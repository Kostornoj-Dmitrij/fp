using System.Drawing;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.ResultPattern;

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

    public Result<Color> GetColor()
    {
        if (_colorGetterProperties.ColorName == "random")
        {
            return Result.Ok(Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255)));
        }

        if (WellKnownColors.Colors.TryGetValue(_colorGetterProperties.ColorName, out var customColor))
        {
            return Result.Ok(customColor);
        }
        var color = Color.FromName(_colorGetterProperties.ColorName); 
        if (color.IsKnownColor)
            return Result.Ok(color);
        
        return Result.Fail<Color>($"Color '{_colorGetterProperties.ColorName}' is not found in the color database.");
    }
}