using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Readers;

public class TxtReader : IReader
{
    public bool CanRead(string pathToFile)
    {
        var isTxtFile = pathToFile.Split('.')[^1].Equals("txt", StringComparison.InvariantCultureIgnoreCase);
        var fileExists = File.Exists(pathToFile);
        return isTxtFile && fileExists;
    }

    public Result<List<string>> Read(string pathToFile)
    {
        try
        {
            var paragraphs = File.ReadAllText(pathToFile)
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var words = WordsGetter.GetWords(paragraphs);
            return Result.Ok(words);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<string>>($"Failed to read .txt file: {ex.Message}");
        }
    }
}