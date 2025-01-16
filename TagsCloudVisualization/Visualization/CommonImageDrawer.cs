using System.Drawing;
using TagsCloudVisualization.ColorGetter;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.ResultPattern;
using TagsCloudVisualization.TagLayouters;

namespace TagsCloudVisualization.Visualization;

public class CommonImageDrawer : IImageDrawer
{
    private ImageProperties _imageProperties;
    private readonly IColorGetter _colorGenerator;

    public CommonImageDrawer(ImageProperties imageProperties, IColorGetter colorGenerator)
    {
        _imageProperties = imageProperties;
        _colorGenerator = colorGenerator;
    }

    public Result<Bitmap> Draw(IEnumerable<Tag> tags)
    {
        var enumerable = tags.ToList();
        var (minX, minY, maxX, maxY) = CalculateBounds(enumerable);
        _imageProperties.Width = maxX - minX + 20;
        _imageProperties.Height = maxY - minY + 20;

        var bitmap = new Bitmap(_imageProperties.Width, _imageProperties.Height);
        var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(_imageProperties.BackgroundColor);

        foreach (var tag in enumerable)
        {
            var font = new Font(tag.Font, tag.Size);
            var colorResult = _colorGenerator.GetColor();
            var color = new SolidBrush(Color.Black);
            if (!colorResult.IsSuccess)
                Console.WriteLine($"Failed to get color for tag '{tag.Content}': {colorResult.Error}");
            else
                color = new SolidBrush(colorResult.GetValueOrThrow());

            var rectangle = tag.Rectangle with
            {
                X = tag.Rectangle.X - minX + 10,
                Y = tag.Rectangle.Y - minY + 10
            };

            graphics.DrawString(tag.Content, font, color, rectangle);
        }
        return Result.Ok(bitmap);
    }

    private (int minX, int minY, int maxX, int maxY) CalculateBounds(IEnumerable<Tag> tags)
    {
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        var maxX = int.MinValue;
        var maxY = int.MinValue;

        var dummyBitmap = new Bitmap(1, 1);
        var graphics = Graphics.FromImage(dummyBitmap);

        foreach (var tag in tags)
        {
            var font = new Font(tag.Font, tag.Size);
            var textSize = graphics.MeasureString(tag.Content, font);

            var tagWidth = (int)textSize.Width;
            var tagHeight = (int)textSize.Height;

            minX = Math.Min(minX, tag.Rectangle.X);
            minY = Math.Min(minY, tag.Rectangle.Y);
            maxX = Math.Max(maxX, tag.Rectangle.X + tagWidth);
            maxY = Math.Max(maxY, tag.Rectangle.Y + tagHeight);
        }

        return (minX, minY, maxX, maxY);
    }
}