namespace TagsCloudVisualization.TagLayouters;

public interface ITagLayouter
{
    IEnumerable<Tag> GetTags();
}