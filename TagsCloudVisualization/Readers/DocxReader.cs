using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.Readers;

public class DocxReader : IReader
{
    public bool CanRead(string path)
    {
        var isDocxFile = path.Split('.')[^1].Equals("docx", StringComparison.InvariantCultureIgnoreCase);
        var fileExists = File.Exists(path);
        return isDocxFile && fileExists;
    }

    public Result<List<string>> Read(string path)
    {
        try
        {
            using var doc = WordprocessingDocument.Open(path, false);

            var body = doc.MainDocumentPart?.Document.Body;
            if (body == null)
                return Result.Fail<List<string>>("Failed to read .docx file: Document body is null.");

            var paragraphs = body.Descendants<Text>().Select(text => text.Text);
            var words = WordsGetter.GetWords(paragraphs);

            return Result.Ok(words);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<string>>($"Failed to read .docx file: {ex.Message}");
        }
    }
}