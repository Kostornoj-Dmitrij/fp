namespace TagsCloudVisualization.Readers;

public interface IReader
{
    bool CanRead(string pathToFile);

    List<string> Read(string pathToFile);
}