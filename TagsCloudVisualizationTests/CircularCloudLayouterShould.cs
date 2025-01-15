using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Layouts;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualizationTests.Utils;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouterShould
{
    private ICircularCloudLayouter _cloudLayouter;
    private List<Rectangle> _rectangles;

    [SetUp]
    public void Setup()
    {
        _cloudLayouter = new CircularCloudLayouter(new CircularLayout(new CircularLayoutProperties()));
        _rectangles = [];
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) 
            return;

        var savePath = TestContext.CurrentContext.TestDirectory + @"\imageFailedTests";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        var testName = TestContext.CurrentContext.Test.Name;
        var bitmap = ImageDrawer.DrawLayout(_rectangles, 10);
        var imageSaver = new CommonImageSaver(new SaveProperties(savePath, testName, "png"));

        imageSaver.Save(bitmap);

        Console.WriteLine($@"Tag cloud visualization was saved: {savePath}\{testName}.png");
    }

    [TestCase(10, 1, 25)]
    [TestCase(25, 1, 50)]
    [TestCase(50, 1, 150)]
    [TestCase(100, 1, 200)]
    public void CircularCloudLayouter_ShouldNotPutIntersectedRectangles(int countRectangles, 
        int minSideLength, int maxSideLength)
    {
        var rectangleSizes = GeometryCalculator
                                    .GenerateRectangleSizes(countRectangles, minSideLength, maxSideLength);

        _rectangles.AddRange(rectangleSizes.Select(x => _cloudLayouter.PutNextRectangle(x)));

        for (var i = 0; i < _rectangles.Count-1; i++)
        {
            _rectangles.Skip(i + 1)
                .Any(rectangle => rectangle.IntersectsWith(_rectangles[i]))
                .Should()
                .BeFalse();
        }
    }

    [TestCase(10, 1, 25)]
    [TestCase(25, 1, 50)]
    [TestCase(50, 1, 150)]
    [TestCase(100, 1, 200)]
    public void CircularCloudLayouter_ShouldPutRectanglesCloseToCircle(int countRectangles, int minSideLength, 
        int maxSideLength)
    {
        var rectangleSizes = GeometryCalculator
                                    .GenerateRectangleSizes(countRectangles, minSideLength, maxSideLength);

        _rectangles.AddRange(rectangleSizes.Select(t => _cloudLayouter.PutNextRectangle(t)));

        var distances = _rectangles.Select(rectangle => 
                GeometryCalculator.CalculateDistanceBetweenRectangleAndCloudCenter(rectangle, new Point(0, 0)))
            .ToArray();

        for (var i = 1; i < distances.Length; i++)
        {
            var distanceBetweenRectangles =
                GeometryCalculator.CalculateDistanceBetweenRectangles(_rectangles[i], _rectangles[i - 1]);
            distances[i].Should().BeApproximately(distances[i - 1], distanceBetweenRectangles);
        }
    }
}