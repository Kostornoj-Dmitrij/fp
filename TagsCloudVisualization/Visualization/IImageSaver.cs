using System.Drawing;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Visualization;

public interface IImageSaver
{
    Result<None> Save(Bitmap bitmap);
}