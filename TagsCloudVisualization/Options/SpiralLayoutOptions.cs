using CommandLine;
using TagsCloudVisualization.Properties;

namespace TagsCloudVisualization.Options;

public class SpiralLayoutOptions
{
    [Option(ParameterNames.AngleIncreasingStep, Default = CircularLayoutProperties.OneDegree, 
        HelpText = "Delta angle for the spiral")]
    public double AngleIncreasingStep { get; set; }

    [Option(ParameterNames.RadiusIncreasingStep, Default = 1, HelpText = "Delta radius for the spiral")]
    public double RadiusIncreasingStep { get; set; }
}