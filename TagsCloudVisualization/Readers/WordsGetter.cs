namespace TagsCloudVisualization.Readers;

public class WordsGetter
{
    public static List<string> GetWords(IEnumerable<string> paragraphsOfText)
    {
        var words = new List<string>();

        foreach (var paragraph in paragraphsOfText)
        {
            var paragraphWords = paragraph
                .Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?', '"', '(', ')', 
                        '[', ']', '{', '}', '-', '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.Trim());

            words.AddRange(paragraphWords);
        }

        return words;
    }
}