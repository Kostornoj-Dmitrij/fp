using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouts;
using TagsCloudVisualization.Properties;
using TagsCloudVisualizationTests.Utils;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularLayoutShould
{
    [TestCase(11)]
    [TestCase(57)]
    public void CircularLayout_ShouldIncreaseRadiusCorrectly_WhenCalculatePoints(int radiusIncreasingStep)
    {
        var properties = new CircularLayoutProperties(radiusIncreasingStep: radiusIncreasingStep);
        var circularLayout =
            CircularLayout.Create(properties).GetValueOrThrow();

        var firstPoint = circularLayout.CalculateNextPoint();
        var secondPoint = circularLayout.CalculateNextPoint();
        var distanceBetweenPoints = GeometryCalculator.CalculateDistanceBetweenPoints(firstPoint, secondPoint);

        distanceBetweenPoints.Should().Be(radiusIncreasingStep);
    }

    [TestCase(Math.PI / 2)]
    [TestCase(Math.PI / 3)]
    public void CircularLayout_ShouldIncreaseAngleCorrectly_WhenCalculatePoints(double stepIncreasingAngle)
    {
        var radiusIncreasingStep = 1;
        var center = new Point(0, 0);
        var circularLayout = CircularLayout.Create(
            new CircularLayoutProperties(stepIncreasingAngle, radiusIncreasingStep)).GetValueOrThrow();

        var firstPoint = circularLayout.CalculateNextPoint();
        var secondPoint = circularLayout.CalculateNextPoint();
        var thirdPoint = circularLayout.CalculateNextPoint();
        var distanceBetweenCenterAndFirstPoint = GeometryCalculator
                                                            .CalculateDistanceBetweenPoints(center, secondPoint);
        var distanceBetweenCenterAndSecondPoint = GeometryCalculator
                                                            .CalculateDistanceBetweenPoints(center, secondPoint);
        var distanceBetweenCenterAndThirdPoint = GeometryCalculator
                                                            .CalculateDistanceBetweenPoints(center, thirdPoint);

        secondPoint.Should().NotBe(thirdPoint);

        distanceBetweenCenterAndSecondPoint.Should().BeLessThanOrEqualTo(radiusIncreasingStep);
        distanceBetweenCenterAndThirdPoint.Should().BeLessThanOrEqualTo(radiusIncreasingStep);
    }
}