using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Readers;

public interface IReader
{
    bool CanRead(string pathToFile);

    Result<List<string>> Read(string pathToFile);
}