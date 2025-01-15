using System.Drawing;

namespace TagsCloudVisualizationTests.Utils;

public class GeometryCalculator
{
    public static double CalculateDistanceBetweenRectangleAndCloudCenter(Rectangle rectangle, Point center)
    {
        var rectangleCenter = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);

        return CalculateDistanceBetweenPoints(rectangleCenter, center);
    }

    public static double CalculateDistanceBetweenRectangles(Rectangle rectangle1, Rectangle rectangle2)
    {
        var rectangleCenter1 = new Point(rectangle1.X + rectangle1.Width / 2, rectangle1.Y + rectangle1.Height / 2);
        var rectangleCenter2 = new Point(rectangle2.X + rectangle2.Width / 2, rectangle2.Y + rectangle2.Height / 2);

        return CalculateDistanceBetweenPoints(rectangleCenter1, rectangleCenter2);
    }

    public static double CalculateDistanceBetweenPoints(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
    }

    public static List<Size> GenerateRectangleSizes(int countRectangles, int minSideLength, int maxSideLength)
    {
        var random = new Random();

        var generatedSizes = Enumerable.Range(0, countRectangles)
            .Select(_ => new Size(
                random.Next(minSideLength, maxSideLength), 
                random.Next(minSideLength, maxSideLength))
            )
            .ToList();

        return generatedSizes;
    }
}