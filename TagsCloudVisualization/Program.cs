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
        try
        {
            var container = DiContainer.Configure(commandLineOptions);
            var cloudMaker = container.Resolve<TagsCloudMaker>();
            var image = cloudMaker.MakeImage();
            var imageSaver = container.Resolve<IImageSaver>();

            imageSaver.Save(image);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
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