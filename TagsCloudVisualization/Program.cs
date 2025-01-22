using Autofac;
using CommandLine;
using TagsCloudVisualization.DiConfiguration;
using TagsCloudVisualization.Options;
using TagsCloudVisualization.ResultPattern;
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
        try
        {
            commandLineOptions
                .Validate()
                .Then(DiContainer.Configure)
                .Then(container =>
                {
                    var cloudMaker = container.Resolve<TagsCloudMaker>();
                    return cloudMaker.MakeImage();
                })
                .Then(image =>
                {
                    var containerResult = DiContainer.Configure(commandLineOptions).Value!;
                    var imageSaver = containerResult.Resolve<IImageSaver>();
                    return imageSaver.Save(image);
                })
                .OnFail(error =>
                {
                    Console.WriteLine($"Error: {error}");
                    Environment.Exit(1);
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unhandled exception: {ex.Message}");
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