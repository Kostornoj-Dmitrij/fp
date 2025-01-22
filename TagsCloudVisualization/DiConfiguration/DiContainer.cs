using Autofac;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.ColorGetter;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Layouts;
using TagsCloudVisualization.Options;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.ResultPattern;
using TagsCloudVisualization.TagLayouters;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.DiConfiguration;

public static class DiContainer
{
    public static Result<IContainer> Configure(CommandLineOptions options)
    {
        try
        {
            var builder = new ContainerBuilder();

            RegisterSettings(builder, options);
            RegisterInternalServices(builder);
            RegisterTagCloudComponents(builder);

            var container = builder.Build();
            return Result.Ok(container);
        }
        catch (Exception ex)
        {
            return Result.Fail<IContainer>("An error occurred during application configuration." +
                                           " Please check your input data and try again.");
        }
    }

    private static void RegisterSettings(ContainerBuilder builder, CommandLineOptions options)
    {
        builder.RegisterType<TextHandlerProperties>().WithParameters(new[]
        {
            new NamedParameter("pathToBoringWords", options.PathToBoringWords),
            new NamedParameter("pathToText", options.PathToText)
        });

        builder.RegisterType<ColorGetterProperties>().WithParameters(new[]
        {
            new NamedParameter("colorName", options.Color)
        });

        builder.RegisterType<SaveProperties>().WithParameters(new[]
        {
            new NamedParameter("filePath", options.PathToSaveDirectory),
            new NamedParameter("fileName", options.FileName),
            new NamedParameter("fileFormat", options.FileFormat)
        });

        builder.RegisterType<ImageProperties>().WithParameters(new[]
        {
            new NamedParameter("width", options.ImageWidth),
            new NamedParameter("height", options.ImageHeight),
            new NamedParameter("colorName", options.BackgroundColor)
        });

        builder.RegisterType<CircularLayoutProperties>().WithParameters(new[]
        {
            new NamedParameter("AngleIncreasingStep", options.SpiralLayout.AngleIncreasingStep),
            new NamedParameter("RadiusIncreasingStep", options.SpiralLayout.RadiusIncreasingStep)
        });

        builder.RegisterType<TagLayouterProperties>().WithParameters(new[]
        {
            new NamedParameter("fontName", options.Font),
            new NamedParameter("minSize", options.MinFontSize),
            new NamedParameter("maxSize", options.MaxFontSize)
        });
    }

    private static void RegisterInternalServices(ContainerBuilder builder)
    {
        builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
        builder.RegisterType<ColorGetter.ColorGetter>().As<IColorGetter>();
        builder.RegisterType<TxtReader>().As<IReader>();
        builder.RegisterType<DocReader>().As<IReader>();
        builder.RegisterType<DocxReader>().As<IReader>();
        builder.RegisterType<CommonImageDrawer>().As<IImageDrawer>();
        builder.RegisterType<CommonImageSaver>().As<IImageSaver>();
        builder.RegisterType<TextHandler>().As<ITextHandler>();
    }

    private static void RegisterTagCloudComponents(ContainerBuilder builder)
    {
        builder.RegisterType<TagLayouter>().As<ITagLayouter>();
        builder.RegisterType<TagsCloudMaker>();

        builder.Register(c =>
        {
            var layoutProperties = c.Resolve<CircularLayoutProperties>();
            return CircularLayout.Create(layoutProperties).GetValueOrThrow();
        }).As<ILayout>();
    }
}