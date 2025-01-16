using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Visualization;

public class CommonImageSaver : IImageSaver
{
    private readonly SaveProperties _properties;

    public CommonImageSaver(SaveProperties properties)
    {
        _properties = properties;
    }

    public Result<None> Save(Bitmap bitmap)
    {
        var fileName = _properties.FileName;
        var projectDirectory = Directory.GetParent(
            Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                        AppDomain.CurrentDomain.BaseDirectory)!.FullName)!.FullName)!.FullName)!.FullName;
        var relativePath = Path.Combine(projectDirectory, "Images");
        var imageFormatResult = GetImageFormat(_properties.FileFormat);

        if (!imageFormatResult.IsSuccess)
            return Result.Fail<None>(imageFormatResult.Error);

        if (!Directory.Exists(relativePath))
        {
            Directory.CreateDirectory(relativePath);
        }

        bitmap.Save(Path.Combine(relativePath, $"{fileName}.{_properties.FileFormat}"), imageFormatResult.Value);
        return Result.Ok();
    }

    private static Result<ImageFormat> GetImageFormat(string format)
    {
        if (string.IsNullOrEmpty(format))
            return Result.Fail<ImageFormat>("Format cannot be null or empty");

        var imageFormatConverter = new ImageFormatConverter();
        var imageFormat = imageFormatConverter.ConvertFromString(format);

        if (imageFormat is null)
            return Result.Fail<ImageFormat>($"Can't handle format: {format}");

        return Result.Ok((ImageFormat)imageFormat);
    }
}