using System.Drawing;

namespace TagsCloudVisualization.Visualization;

public interface IImageSaver
{
    void Save(Bitmap bitmap);
}