using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.ColorGetter;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.Readers;


namespace TagsCloudVisualizationTests;

[TestFixture]
public class ResultPatternShould
{
    [Test]
    public void GetColor_ShouldReturnError_WhenColorNameIsNotFound()
    {
        var properties = new ColorGetterProperties("UnknownColor");
        var colorGetter = new ColorGetter(properties);

        var result = colorGetter.GetColor();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Color 'UnknownColor' is not found in the color database.");
    }
    
    [TestCase("Files/nonexistent.txt", "Files/txtFile.txt", "There is no file in Files/nonexistent.txt")]
    [TestCase("Files/txtFile.txt", "Files/nonexistent_boring.txt", "There is no file in Files/nonexistent_boring.txt")]
    public void GetWordsCount_ShouldReturnError_WhenFileNotFound(string textFilePath, string boringWordsFilePath, string expectedError)
    {
        var readers = new IReader[] { new TxtReader(), new DocReader() };
        var properties = new TextHandlerProperties(textFilePath, boringWordsFilePath);
        var textHandler = new TextHandler(readers, properties);

        var result = textHandler.GetWordsCount();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(expectedError);
    }
}