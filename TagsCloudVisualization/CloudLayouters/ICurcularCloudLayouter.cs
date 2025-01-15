using System.Drawing;

namespace TagsCloudVisualization.CloudLayouters;

public interface ICircularCloudLayouter
{
    Rectangle PutNextRectangle(Size rectangleSize);
}