using System.Drawing;

namespace TagsCloudVisualizationTests.Utils;

public class ImageDrawer
{
    public static Bitmap DrawLayout(List<Rectangle> rectangles, int borderPadding)
    {
        var minX = rectangles.Min(rectangle => rectangle.Left);
        var minY = rectangles.Min(rectangle => rectangle.Top);
        var maxX = rectangles.Max(rectangle => rectangle.Right);
        var maxY = rectangles.Max(rectangle => rectangle.Bottom);

        var width = maxX - minX + borderPadding;
        var height = maxY - minY + borderPadding;

        var random = new Random();
        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(Color.White);

        foreach (var rectangle in rectangles)
        {
            var movedRectangle = rectangle with
            {
                X = rectangle.X - minX + borderPadding, 
                Y = rectangle.Y - minY + borderPadding
            };

            var randomColor = Color.FromArgb(random.Next(255), 
                random.Next(255), random.Next(255));
            var brush = new SolidBrush(randomColor);

            graphics.FillRectangle(brush, movedRectangle);
            graphics.DrawRectangle(Pens.Black, movedRectangle);
        }
        return bitmap;
    }
}