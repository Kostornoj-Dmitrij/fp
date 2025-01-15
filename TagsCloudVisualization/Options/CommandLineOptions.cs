using CommandLine;

namespace TagsCloudVisualization.Options;

public class CommandLineOptions
{
    [Option('t', "pathToText", Default = "textForCloud.txt", HelpText = "Path to text for tags cloud")]
    public string? PathToText { get; set; }

    [Option('s', "pathToSaveDirectory", Default = "/Images", 
                                                            HelpText = "Where we will save images to")]
    public string? PathToSaveDirectory { get; set; }

    [Option('n', "fileName", Default = "image", HelpText = "Name of the file to save")]
    public string? FileName { get; set; }

    [Option('b', "backgroundColor", Default = "white", HelpText = "Color of image background")]
    public string? BackgroundColor { get; set; }

    [Option("color", Default = "random", HelpText = "Color of the words (Random by default)")]
    public string? Color { get; set; }

    [Option("fileFormat", Default = "png", HelpText = "Format of file to save (.png by default)")]
    public string? FileFormat { get; set; }

    [Option("imageWidth", Default = 1920, HelpText = "Width of image")]
    public int ImageWidth { get; set; }

    [Option("imageHeight", Default = 1080, HelpText = "Height of image")]
    public int ImageHeight { get; set; }

    [Option("pathToBoringWords", Default = "BoringWords.txt", HelpText = "Path to boring words for skip")]
    public string? PathToBoringWords { get; set; }

    [Option("font", Default = "Times New Roman", HelpText = "Font of the words")]
    public string? Font { get; set; }

    [Option("minFontSize", Default = 10, HelpText = "Minimum font size for word")]
    public int MinFontSize { get; set; }

    [Option("maxFontSize", Default = 50, HelpText = "Maximum word font size for word")]
    public int MaxFontSize { get; set; }

    public SpiralLayoutOptions SpiralLayout { get; set; } = new SpiralLayoutOptions();
}