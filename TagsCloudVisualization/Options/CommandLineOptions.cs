using CommandLine;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Options;

public class CommandLineOptions
{
    [Option('t', ParameterNames.PathToText, Default = "textForCloud.txt", HelpText = "Path to text for tags cloud")]
    public string? PathToText { get; set; }

    [Option('s', ParameterNames.PathToSaveDirectory, Default = "/Images",
        HelpText = "Where we will save images to")]
    public string? PathToSaveDirectory { get; set; }

    [Option('n', ParameterNames.FileName, Default = "image", HelpText = "Name of the file to save")]
    public string? FileName { get; set; }

    [Option('b', ParameterNames.BackgroundColor, Default = "white", HelpText = "Color of image background")]
    public string? BackgroundColor { get; set; }

    [Option(ParameterNames.Color, Default = "random", HelpText = "Color of the words (Random by default)")]
    public string? Color { get; set; }

    [Option(ParameterNames.FileFormat, Default = "png", HelpText = "Format of file to save (.png by default)")]
    public string? FileFormat { get; set; }

    [Option(ParameterNames.Width, Default = 1920, HelpText = "Width of image")]
    public int ImageWidth { get; set; }

    [Option(ParameterNames.Height, Default = 1080, HelpText = "Height of image")]
    public int ImageHeight { get; set; }

    [Option(ParameterNames.PathToBoringWords, Default = "BoringWords.txt", HelpText = "Path to boring words for skip")]
    public string? PathToBoringWords { get; set; }

    [Option(ParameterNames.Font, Default = "Times New Roman", HelpText = "Font of the words")]
    public string? Font { get; set; }

    [Option(ParameterNames.MinFontSize, Default = 10, HelpText = "Minimum font size for word")]
    public int MinFontSize { get; set; }

    [Option(ParameterNames.MaxFontSize, Default = 50, HelpText = "Maximum word font size for word")]
    public int MaxFontSize { get; set; }

    public SpiralLayoutOptions SpiralLayout { get; set; } = new SpiralLayoutOptions();

    public Result<CommandLineOptions> Validate()
    {
        return ValidateImageDimensions()
            .Then(_ => ValidateFontSizes())
            .Then(_ => ValidateFontSizeRange())
            .Then(_ => ValidateSpiralLayoutSteps());
    }

    private Result<CommandLineOptions> ValidateImageDimensions()
    {
        if (ImageWidth < 0 || ImageHeight < 0)
            return Result.Fail<CommandLineOptions>("Image width and height must be positive.");
        return Result.Ok(this);
    }

    private Result<CommandLineOptions> ValidateFontSizes()
    {
        if (MinFontSize < 0 || MaxFontSize < 0)
            return Result.Fail<CommandLineOptions>("Font sizes must be positive.");
        return Result.Ok(this);
    }

    private Result<CommandLineOptions> ValidateFontSizeRange()
    {
        if (MinFontSize > MaxFontSize)
            return Result.Fail<CommandLineOptions>("MinFontSize must be less than or equal to MaxFontSize.");
        return Result.Ok(this);
    }

    private Result<CommandLineOptions> ValidateSpiralLayoutSteps()
    {
        if (SpiralLayout.AngleIncreasingStep < 0)
            return Result.Fail<CommandLineOptions>("AngleIncreasingStep must be positive.");
        if (SpiralLayout.RadiusIncreasingStep < 0)
            return Result.Fail<CommandLineOptions>("RadiusIncreasingStep must be positive.");
        return Result.Ok(this);
    }
}