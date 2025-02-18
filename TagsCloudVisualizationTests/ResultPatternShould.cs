using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.ColorGetter;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Layouts;
using TagsCloudVisualization.Options;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.TagLayouters;


namespace TagsCloudVisualizationTests;

[TestFixture]
public class ResultPatternShould
{
    private IReader[] _readers;
    private TextHandlerProperties _textHandlerProperties;
    private TextHandler _textHandler;
    private ICircularCloudLayouter _circularCloudLayouter;

    [SetUp]
    public void SetUp()
    {
        _readers = new IReader[] { new TxtReader(), new DocReader() };
        _textHandlerProperties = new TextHandlerProperties("Files/txtFile.txt", 
                                                                    "Files/txtFile.txt");
        _textHandler = new TextHandler(_readers, _textHandlerProperties);
        _circularCloudLayouter = new CircularCloudLayouter(
            CircularLayout.Create(new CircularLayoutProperties()).GetValueOrThrow()
        );
    }

    [Test]
    public void CircularLayout_ShouldReturnError_WhenAngleIncreasingStepIsZero()
    {
        var properties = new CircularLayoutProperties(angleIncreasingStep: 0);

        var result = CircularLayout.Create(properties);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be($"AngleIncreasingStep should not be zero. Provided value: 0");
    }

    [TestCase(0)]
    [TestCase(-2)]
    public void CircularLayout_ShouldReturnError_WhenRadiusIncreasingStepIsInvalid(int radiusIncreasingStep)
    {
        var properties = new CircularLayoutProperties(radiusIncreasingStep: radiusIncreasingStep);

        var result = CircularLayout.Create(properties);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be($"RadiusIncreasingStep should be positive. Provided value: {radiusIncreasingStep}");
    }

    [Test]
    public void GetColor_ShouldReturnError_WhenColorNameIsNotFound()
    {
        var properties = new ColorGetterProperties("UnknownColor");
        var colorGetter = new ColorGetter(properties);

        var result = colorGetter.GetColor();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Color 'UnknownColor' is not found in the color database.");
    }
    
    [TestCase("Files/nonexistent.txt", 
        "Files/txtFile.txt", 
        "There is no file in Files/nonexistent.txt")]
    [TestCase("Files/txtFile.txt", 
        "Files/nonexistent_boring.txt", 
        "There is no file in Files/nonexistent_boring.txt")]
    public void GetWordsCount_ShouldReturnError_WhenFileNotFound(string file, string boringFile, string expectedError)
    {
        var properties = new TextHandlerProperties(file, boringFile);
        var textHandler = new TextHandler(_readers, properties);

        var result = textHandler.GetWordsCount();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(expectedError);
    }
    
    [Test]
    public void Validate_ShouldReturnError_WhenImageWidthIsNegative()
    {
        var options = new CommandLineOptions { ImageWidth = -100, ImageHeight = 1080 };

        var result = options.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Image width and height must be positive.");
    }

    [Test]
    public void Validate_ShouldReturnError_WhenMinFontSizeIsGreaterThanMaxFontSize()
    {
        var options = new CommandLineOptions { MinFontSize = 50, MaxFontSize = 10 };

        var result = options.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("MinFontSize must be less than or equal to MaxFontSize.");
    }

    [Test]
    public void Validate_ShouldReturnError_WhenAngleIncreasingStepIsNegative()
    {
        var options = new CommandLineOptions
        {
            SpiralLayout = new SpiralLayoutOptions { AngleIncreasingStep = -12 }
        };

        var result = options.Validate();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("AngleIncreasingStep must be positive.");
    }

    [Test]
    public void Validate_ShouldReturnOk_WhenAllPropertiesAreValid()
    {
        var options = new CommandLineOptions
        {
            ImageWidth = 1920,
            ImageHeight = 1080,
            MinFontSize = 10,
            MaxFontSize = 50,
            SpiralLayout = new SpiralLayoutOptions { AngleIncreasingStep = 0.1, RadiusIncreasingStep = 1 }
        };

        var result = options.Validate();

        result.IsSuccess.Should().BeTrue();
        result.GetValueOrThrow().Should().Be(options);
    }
    
    [Test]
    public void GetTags_ShouldReturnError_WhenFontIsNotFound()
    {
        var fontName = "NonExistentFont";
        var properties = new TagLayouterProperties(10, 50, fontName);
        var tagLayouter = new TagLayouter(_circularCloudLayouter, _textHandler, properties);

        var result = tagLayouter.GetTags();

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be($"Font '{fontName}' not found in the system.");
    }
}