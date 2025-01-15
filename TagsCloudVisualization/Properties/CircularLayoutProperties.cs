namespace TagsCloudVisualization.Properties;

public class CircularLayoutProperties
{
    public double AngleIncreasingStep { get; }
    public int RadiusIncreasingStep { get; }
    public const double OneDegree = Math.PI / 180;

    public CircularLayoutProperties(double angleIncreasingStep = OneDegree, int radiusIncreasingStep = 1)
    {
        AngleIncreasingStep = angleIncreasingStep;
        RadiusIncreasingStep = radiusIncreasingStep;
    }
}