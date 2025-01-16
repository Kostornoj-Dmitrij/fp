using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.TagLayouters;

public interface ITagLayouter
{
    Result<IEnumerable<Tag>> GetTags();
}