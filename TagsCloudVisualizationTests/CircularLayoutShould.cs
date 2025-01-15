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
    [Test]
    public void CircularLayout_ShouldThrowArgumentException_WhenAngleIncreasingStepIsZero()
    {
        var properties = new CircularLayoutProperties(angleIncreasingStep: 0);

        var act = () => { _ = new CircularLayout(properties); };

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"AngleIncreasingStep should not be zero. Provided value: 0");
    }

    [TestCase(0)]
    [TestCase(-2)]
    public void CircularLayout_ShouldThrowArgumentException_WhenRadiusIncreasingStepIsInvalid(int radiusIncreasingStep)
    {
        var properties = new CircularLayoutProperties(radiusIncreasingStep: radiusIncreasingStep);

        var act = () => { _ = new CircularLayout(properties); };

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage($"RadiusIncreasingStep should be positive. Provided value: {radiusIncreasingStep}");
    }

    [TestCase(11)]
    [TestCase(57)]
    public void CircularLayout_ShouldIncreaseRadiusCorrectly_WhenCalculatePoints(int radiusIncreasingStep)
    {
        var properties = new CircularLayoutProperties(radiusIncreasingStep: radiusIncreasingStep);
        var circularLayout =
            new CircularLayout(properties);

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
        var circularLayout = new CircularLayout(
            new CircularLayoutProperties(stepIncreasingAngle, radiusIncreasingStep));

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