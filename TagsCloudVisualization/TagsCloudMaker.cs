using System.Drawing;
using TagsCloudVisualization.ResultPattern;
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

    public Result<Bitmap> MakeImage()
    {
        var tagsResult = _tagLayouter.GetTags();
        if (!tagsResult.IsSuccess)
            return Result.Fail<Bitmap>(tagsResult.Error);

        var bitmapResult = _imageDrawer.Draw(tagsResult.Value);
        if (!bitmapResult.IsSuccess)
            return Result.Fail<Bitmap>(bitmapResult.Error);

        return bitmapResult;
    }
}