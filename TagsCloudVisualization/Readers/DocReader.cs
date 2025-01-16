using Spire.Doc;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Readers;

public class DocReader : IReader
{
    public bool CanRead(string pathToFile)
    {
        var isDocFile = pathToFile.Split('.')[^1].Equals("doc", StringComparison.InvariantCultureIgnoreCase);
        var fileExists = File.Exists(pathToFile);
        return isDocFile && fileExists;
    }

    public Result<List<string>> Read(string pathToFile)
    {
        try
        {
            var doc = new Document();
            doc.LoadFromFile(pathToFile);

            var text = doc.GetText();
            var paragraphs = text
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1);

            var words = WordsGetter.GetWords(paragraphs);
            return Result.Ok(words);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<string>>($"Failed to read .doc file: {ex.Message}");
        }
    }
}