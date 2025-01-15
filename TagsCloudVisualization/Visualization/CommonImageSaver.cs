using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Properties;

namespace TagsCloudVisualization.Visualization;

public class CommonImageSaver : IImageSaver
{
    private readonly SaveProperties _properties;

    public CommonImageSaver(SaveProperties properties)
    {
        _properties = properties;
    }

    public void Save(Bitmap bitmap)
    {
        var fileName = _properties.FileName;
        var projectDirectory = Directory.GetParent(
            Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                        AppDomain.CurrentDomain.BaseDirectory)!.FullName)!.FullName)!.FullName)!.FullName;
        var relativePath = Path.Combine(projectDirectory, "Images");
        var imageFormat = GetImageFormat(_properties.FileFormat);

        if (!Directory.Exists(relativePath))
        {
            Directory.CreateDirectory(relativePath);
        }
        bitmap.Save(Path.Combine(relativePath, $"{fileName}.{_properties.FileFormat}"), imageFormat);
    }

    private static ImageFormat GetImageFormat(string format)
    {
        try
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentException("Format cannot be null or empty");

            var imageFormatConverter = new ImageFormatConverter();
            var imageFormat = imageFormatConverter.ConvertFromString(format);

            if (imageFormat is null)
                throw new ArgumentException($"Can't handle format: {format}");
            return (ImageFormat)imageFormat;
        }
        catch (NotSupportedException)
        {
            throw new NotSupportedException($"File with format {format} doesn't supported");
        }
    }
}