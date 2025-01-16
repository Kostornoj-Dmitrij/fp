using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Handlers;

public interface ITextHandler
{
    Result<Dictionary<string, int>> GetWordsCount();
}