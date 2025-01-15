namespace TagsCloudVisualization.Properties;

public class TextHandlerProperties
{
    public string PathToBoringWords { get; }
    public string PathToText { get; }

    public TextHandlerProperties(string pathToBoringWords, string pathToText)
    {
        PathToBoringWords = pathToBoringWords;
        PathToText = pathToText;
    }
}