using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.ResultPattern;

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

    public Result<Dictionary<string, int>> GetWordsCount()
    {
        var readerResult = GetReader(_properties.PathToText);
        if (!readerResult.IsSuccess)
            return Result.Fail<Dictionary<string, int>>(readerResult.Error);

        var reader = readerResult.GetValueOrThrow();
        var words = reader.Read(_properties.PathToText);

        var boringWordsReaderResult = GetReader(_properties.PathToBoringWords);
        if (!boringWordsReaderResult.IsSuccess)
            return Result.Fail<Dictionary<string, int>>(boringWordsReaderResult.Error);

        var boringWordsReader = boringWordsReaderResult.GetValueOrThrow();
        var boringWords = boringWordsReader.Read(_properties.PathToBoringWords).ToHashSet();

        return Result.Ok(
            words.Select(word => word.ToLower())
                .Where(word => !boringWords.Contains(word))
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count())
                .OrderByDescending(pair => pair.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value));
    }

    private Result<IReader> GetReader(string path)
    {
        var reader = _readers.FirstOrDefault(r => r.CanRead(path));
        return reader != null
            ? Result.Ok(reader)
            : Result.Fail<IReader>($"There is no file in {path}");
    }
}