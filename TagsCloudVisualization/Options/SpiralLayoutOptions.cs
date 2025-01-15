using CommandLine;
using TagsCloudVisualization.Properties;

namespace TagsCloudVisualization.Options;

public class SpiralLayoutOptions
{
    [Option("angleIncreasingStep", Default = CircularLayoutProperties.OneDegree, 
        HelpText = "Delta angle for the spiral")]
    public double AngleIncreasingStep { get; set; }

    [Option("radiusIncreasingStep", Default = 1, HelpText = "Delta radius for the spiral")]
    public double RadiusIncreasingStep { get; set; }
}