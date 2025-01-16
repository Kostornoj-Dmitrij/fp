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

        var containerResult = DiContainer.Configure(commandLineOptions);
        if (!containerResult.IsSuccess)
        {
            Console.WriteLine($"DI container configuration error: {containerResult.Error}");
            Environment.Exit(1);
        }
        var container = containerResult.Value!;

        var cloudMaker = container.Resolve<TagsCloudMaker>();
        var imageResult = cloudMaker.MakeImage();
        if (!imageResult.IsSuccess)
        {
            Console.WriteLine($"Failed to create the image: {imageResult.Error}");
            Environment.Exit(1);
        }
        var imageSaver = container.Resolve<IImageSaver>();
        var saveResult = imageSaver.Save(imageResult.Value!);
        if (!saveResult.IsSuccess)
        {
            Console.WriteLine($"Failed to save the image: {saveResult.Error}");
            Environment.Exit(1);
        }
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