using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Properties;

namespace TagsCloudVisualization.Handlers;

public class TextHandler : ITextHandler
{
    private readonly IReader[] _readers;
    private readonly TextHandlerProperties _properties;

    public TextHandler(IReader[] readers, TextHandlerProperties properties)
    {
        _readers = readers;
        _properties = properties;
    }

    public Dictionary<string, int> GetWordsCount()
    {
        var reader = GetReader(_properties.PathToText);
        var words = reader.Read(_properties.PathToText);
        var boringWordsReader = GetReader(_properties.PathToBoringWords);
        var boringWords = boringWordsReader.Read(_properties.PathToBoringWords).ToHashSet();
        var wordsCount = new Dictionary<string, int>();

        foreach (var word in words.Select(word => word.ToLower()))
        {
            if (wordsCount.TryGetValue(word, out var value))
            {
                wordsCount[word] = ++value;
                continue;
            }

            if (!boringWords.Contains(word))
            {
                wordsCount[word] = 1;
            }
        }

        return words
            .Select(word => word.ToLower())
            .Where(word => !boringWords.Contains(word))
            .GroupBy(word => word)
            .ToDictionary(group => group.Key, group => group.Count())
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    private IReader GetReader(string path)
    {
        var reader = _readers.FirstOrDefault(r => r.CanRead(path));

        return reader ?? throw new ArgumentException($"There is no file in {path}");
    }
}