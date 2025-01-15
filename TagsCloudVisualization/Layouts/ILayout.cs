using System.Drawing;

namespace TagsCloudVisualization.Layouts;

public interface ILayout
{
    Point CalculateNextPoint();
}