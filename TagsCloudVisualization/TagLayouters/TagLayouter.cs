using System.Drawing;
using System.Drawing.Text;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Properties;
using TagsCloudVisualization.ResultPattern;

namespace TagsCloudVisualization.TagLayouters;

public class TagLayouter : ITagLayouter
{
    private readonly ICircularCloudLayouter _circularCloudLayouter;
    private readonly ITextHandler _textHandler;
    private readonly TagLayouterProperties _tagLayouterProperties;
    private readonly Graphics _graphics;

    public TagLayouter(ICircularCloudLayouter circularCloudLayouter, ITextHandler textHandler,
                        TagLayouterProperties tagLayouterProperties)
    {
        _textHandler = textHandler;
        _tagLayouterProperties = tagLayouterProperties;
        _circularCloudLayouter = circularCloudLayouter;
        _graphics = Graphics.FromHwnd(IntPtr.Zero);
    }

    public Result<IEnumerable<Tag>> GetTags()
    {
        var wordsCountResult = _textHandler.GetWordsCount();

        if (!wordsCountResult.IsSuccess)
            return Result.Fail<IEnumerable<Tag>>("Error getting words count: " + wordsCountResult.Error);

        if (!IsFontInstalled(_tagLayouterProperties.FontName))
            return Result.Fail<IEnumerable<Tag>>(
                $"Font '{_tagLayouterProperties.FontName}' not found in the system.");

        var wordsCount = wordsCountResult.GetValueOrThrow();
        var minCount = wordsCount.Last().Value;
        var maxCount = wordsCount.First().Value;

        var tags = new List<Tag>();
        foreach (var wordWithCount in wordsCount)
        {
            var fontSize = GetFontSize(minCount, maxCount, wordWithCount.Value);
            tags.Add(new Tag(
                wordWithCount.Key,
                fontSize,
                _circularCloudLayouter.PutNextRectangle(GetWordSize(wordWithCount.Key, fontSize)),
                new FontFamily(_tagLayouterProperties.FontName)
            ));
        }

        return Result.Ok<IEnumerable<Tag>>(tags);
    }

    private int GetFontSize(int minWordCount, int maxWordCount, int wordCount)
    {
        if (maxWordCount > minWordCount)
        {
            return _tagLayouterProperties.MinSize + (_tagLayouterProperties.MaxSize - _tagLayouterProperties.MinSize)
                * (wordCount - minWordCount) / (maxWordCount - minWordCount);
        }

        return _tagLayouterProperties.MaxSize;
    }

    private Size GetWordSize(string content, int fontSize)
    {
        var sizeF = _graphics.MeasureString(content, 
            new Font(new FontFamily(_tagLayouterProperties.FontName), fontSize));

        return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
    
    private bool IsFontInstalled(string fontName)
    {
        foreach (var fontFamily in new InstalledFontCollection().Families)
        {
            if (fontFamily.Name.Equals(fontName, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}