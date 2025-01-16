using Autofac;
using CommandLine;
using TagsCloudVisualization.DiConfiguration;
using TagsCloudVisualization.Options;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization;

public static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsed(RunApplication)
            .WithNotParsed(HandleErrors);
    }

    private static void RunApplication(CommandLineOptions commandLineOptions)
    {
        var validationResult = commandLineOptions.Validate();
        if (!validationResult.IsSuccess)
        {
            Console.WriteLine($"Options validation error: {validationResult.Error}");
            Environment.Exit(1);
        }

        var container = DiContainer.Configure(commandLineOptions);
        var cloudMaker = container.Resolve<TagsCloudMaker>();
        var imageResult = cloudMaker.MakeImage();
        if (!imageResult.IsSuccess)
        {
            Console.WriteLine($"An error occurred: {imageResult.Error}");
            Environment.Exit(1);
        }
        var imageSaver = container.Resolve<IImageSaver>();
        imageSaver.Save(imageResult.Value!);
    }

    private static void HandleErrors(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine(error.ToString());
        }
        Environment.Exit(1);
    }
}