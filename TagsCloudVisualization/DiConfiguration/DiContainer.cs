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
        catch (Exception)
        {
            return Result.Fail<IContainer>("An error occurred during application configuration." +
                                           " Please check your input data and try again.");
        }
    }

    private static void RegisterSettings(ContainerBuilder builder, CommandLineOptions options)
    {
        builder.RegisterType<TextHandlerProperties>().WithParameters(new[]
        {
            new NamedParameter(ParameterNames.PathToBoringWords, options.PathToBoringWords),
            new NamedParameter(ParameterNames.PathToText, options.PathToText)
        });

        builder.RegisterType<ColorGetterProperties>().WithParameters(new[]
        {
            new NamedParameter(ParameterNames.ColorName, options.Color)
        });

        builder.RegisterType<SaveProperties>().WithParameters(new[]
        {
            new NamedParameter(ParameterNames.FilePath, options.PathToSaveDirectory),
            new NamedParameter(ParameterNames.FileName, options.FileName),
            new NamedParameter(ParameterNames.FileFormat, options.FileFormat)
        });

        builder.RegisterType<ImageProperties>().WithParameters(new[]
        {
            new NamedParameter(ParameterNames.Width, options.ImageWidth),
            new NamedParameter(ParameterNames.Height, options.ImageHeight),
            new NamedParameter(ParameterNames.ColorName, options.BackgroundColor)
        });

        builder.RegisterType<CircularLayoutProperties>().WithParameters(new[]
        {
            new NamedParameter(ParameterNames.AngleIncreasingStep, options.SpiralLayout.AngleIncreasingStep),
            new NamedParameter(ParameterNames.RadiusIncreasingStep, options.SpiralLayout.RadiusIncreasingStep)
        });

        builder.RegisterType<TagLayouterProperties>().WithParameters(new[]
        {
            new NamedParameter(ParameterNames.FontName, options.Font),
            new NamedParameter(ParameterNames.MinFontSize, options.MinFontSize),
            new NamedParameter(ParameterNames.MaxFontSize, options.MaxFontSize)
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