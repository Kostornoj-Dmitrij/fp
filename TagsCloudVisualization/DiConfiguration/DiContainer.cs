using Autofac;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.ColorGetter;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Layouts;
using TagsCloudVisualization.Options;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.TagLayouters;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.DiConfiguration;

public class DiContainer
{
    public static IContainer Configure(CommandLineOptions options)
    {
        var builder = new ContainerBuilder();

        builder.Register(c =>
            {
                var layoutProperties = c.Resolve<CircularLayoutProperties>();
                return CircularLayout.Create(layoutProperties).GetValueOrThrow();
            }).As<ILayout>();
        builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
        builder.RegisterType<ColorGetter.ColorGetter>().As<IColorGetter>();
        builder.RegisterType<TxtReader>().As<IReader>();
        builder.RegisterType<DocReader>().As<IReader>();
        builder.RegisterType<DocxReader>().As<IReader>();
        builder.RegisterType<CommonImageDrawer>().As<IImageDrawer>();
        builder.RegisterType<CommonImageSaver>().As<IImageSaver>();
        builder.RegisterType<TextHandler>().As<ITextHandler>();
        builder.RegisterType<TagLayouter>().As<ITagLayouter>();
        builder.RegisterType<TagsCloudMaker>();

        builder.RegisterType<TextHandlerProperties>().WithParameters([
            new NamedParameter("pathToBoringWords", options.PathToBoringWords),
            new NamedParameter("pathToText", options.PathToText)
        ]);

        builder.RegisterType<ColorGetterProperties>().WithParameters([
            new NamedParameter("colorName", options.Color)
        ]);

        builder.RegisterType<SaveProperties>().WithParameters([
            new NamedParameter("filePath", options.PathToSaveDirectory),
            new NamedParameter("fileName", options.FileName),
            new NamedParameter("fileFormat", options.FileFormat)
        ]);

        builder.RegisterType<ImageProperties>().WithParameters([
            new NamedParameter("width", options.ImageWidth),
            new NamedParameter("height", options.ImageHeight),
            new NamedParameter("colorName", options.BackgroundColor)
        ]);

        builder.RegisterType<CircularLayoutProperties>().WithParameters([
            new NamedParameter("AngleIncreasingStep", options.SpiralLayout.AngleIncreasingStep),
            new NamedParameter("RadiusIncreasingStep", options.SpiralLayout.RadiusIncreasingStep)
        ]);

        builder.RegisterType<TagLayouterProperties>().WithParameters([
            new NamedParameter("fontName", options.Font),
            new NamedParameter("minSize", options.MinFontSize),
            new NamedParameter("maxSize", options.MaxFontSize)
        ]);

        return builder.Build();
    }
}