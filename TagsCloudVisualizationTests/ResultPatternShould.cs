using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.ColorGetter;
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
}