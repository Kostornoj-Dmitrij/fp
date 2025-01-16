namespace TagsCloudVisualization.Readers;

public class TxtReader : IReader
{
    public bool CanRead(string pathToFile)
    {
        var isTxtFile = pathToFile.Split('.')[^1].Equals("txt", StringComparison.InvariantCultureIgnoreCase);
        var fileExists = File.Exists(pathToFile);
        return isTxtFile && fileExists;
    }

    public List<string> Read(string pathToFile)
    {
        var paragraphs = File.ReadAllText(pathToFile)
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var words = WordsGetter.GetWords(paragraphs);

        return words;
    }
}