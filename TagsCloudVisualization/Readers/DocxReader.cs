using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudVisualization.Readers;

public class DocxReader : IReader
{
    public bool CanRead(string path)
    {
        var isDocxFile = path.Split('.')[^1].Equals("docx", StringComparison.InvariantCultureIgnoreCase);
        var fileExists = File.Exists(path);
        return isDocxFile && fileExists;
    }

    public List<string> Read(string path)
    {
        using var doc = WordprocessingDocument.Open(path, false);

        var body = doc.MainDocumentPart?.Document.Body;
        var paragraphs = body?.Descendants<Text>().Select(text => text.Text);

        var words = WordsGetter.GetWords(paragraphs!);

        return words;
    }
}