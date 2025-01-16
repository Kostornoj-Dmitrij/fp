using System.Drawing;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.ColorGetter;

public interface IColorGetter
{
    Result<Color> GetColor();
}