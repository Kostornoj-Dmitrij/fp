namespace TagsCloudVisualization.Readers;

public class TxtReader : IReader
{
    public bool CanRead(string pathToFile)
    {
        return pathToFile.Split('.')[^1].Equals("txt", StringComparison.InvariantCultureIgnoreCase);
    }

    public List<string> Read(string pathToFile)
    {
        var paragraphs = File.ReadAllText(pathToFile)
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var words = WordsGetter.GetWords(paragraphs);

        return words;
    }
}