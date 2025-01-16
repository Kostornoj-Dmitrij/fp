using System.Drawing;
using TagsCloudVisualization.ResultPattern;
using TagsCloudVisualization.TagLayouters;

namespace TagsCloudVisualization.Visualization;

public interface IImageDrawer
{
    Result<Bitmap> Draw(IEnumerable<Tag> tags);
}