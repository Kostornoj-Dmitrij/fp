using System.Drawing;
using TagsCloudVisualization.TagLayouters;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization;

public class TagsCloudMaker
{
    private readonly ITagLayouter _tagLayouter;
    private readonly IImageDrawer _imageDrawer;

    public TagsCloudMaker(ITagLayouter tagLayouter, IImageDrawer imageDrawer)
    {
        _tagLayouter = tagLayouter;
        _imageDrawer = imageDrawer;
    }

    public Bitmap MakeImage()
    {
        var tags = _tagLayouter.GetTags();
        var bitmap = _imageDrawer.Draw(tags);

        return bitmap;
    }
}