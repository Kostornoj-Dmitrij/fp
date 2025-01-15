using System.Drawing;
using TagsCloudVisualization.Layouts;

namespace TagsCloudVisualization.CloudLayouters;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly ILayout _layout;
    private readonly List<Rectangle> _rectangles = [];

    public CircularCloudLayouter(ILayout layout)
    {
        _layout = layout;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;

        do
        {
            var nextPoint = _layout.CalculateNextPoint();

            var rectanglePosition = nextPoint - rectangleSize / 2;

            rectangle = new Rectangle(rectanglePosition, rectangleSize);

        } while (IsIntersectWithOtherRectangles(rectangle));

        _rectangles.Add(rectangle);

        return rectangle;
    }

    private bool IsIntersectWithOtherRectangles(Rectangle rectangle)
    {
        return _rectangles.Any(addedRectangle => addedRectangle.IntersectsWith(rectangle));
    }
}