using System.Drawing;
using TagsCloudVisualization.TagLayouters;

namespace TagsCloudVisualization.Visualization;

public interface IImageDrawer
{
    Bitmap Draw(IEnumerable<Tag> tags);
}