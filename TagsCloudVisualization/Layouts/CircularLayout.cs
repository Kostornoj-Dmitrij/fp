using TagsCloudVisualization.Properties;
using System.Drawing;

namespace TagsCloudVisualization.Layouts;

public class CircularLayout : ILayout
{
    private readonly Point _center;
    private readonly double _angleIncreasingStep;
    private readonly int _radiusIncreasingStep;
    private double _circleAngle;
    private double _circleRadius;

    public CircularLayout(CircularLayoutProperties properties)
    {
        if (properties.AngleIncreasingStep == 0)
            throw new ArgumentException($"AngleIncreasingStep should not be zero. Provided value: {properties.AngleIncreasingStep}");

        if (properties.RadiusIncreasingStep <= 0)
            throw new ArgumentException($"RadiusIncreasingStep should be positive. Provided value: {properties.RadiusIncreasingStep}");

        _center = new Point(0, 0);
        _radiusIncreasingStep = properties.RadiusIncreasingStep;
        _angleIncreasingStep = properties.AngleIncreasingStep;
    }

    public Point CalculateNextPoint()
    {
        var x = _center.X + (int)(_circleRadius * Math.Cos(_circleAngle));
        var y = _center.Y + (int)(_circleRadius * Math.Sin(_circleAngle));

        _circleAngle += _angleIncreasingStep; 
        if (_circleAngle > 2 * Math.PI || _circleRadius == 0)
        {
            _circleAngle = 0;
            _circleRadius += _radiusIncreasingStep;
        }

        return new Point(x, y);
    }
}