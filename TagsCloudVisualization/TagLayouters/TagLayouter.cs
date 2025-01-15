using System.Drawing;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Properties;

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

    public IEnumerable<Tag> GetTags()
    {
        var wordsCount = _textHandler.GetWordsCount();
        var minCount = wordsCount.Last().Value;
        var maxCount = wordsCount.First().Value;

        foreach (var wordWithCount in wordsCount)
        {
            var fontSize = GetFontSize(minCount, maxCount, wordWithCount.Value);
            yield return new Tag(wordWithCount.Key, 
                fontSize,
                _circularCloudLayouter.PutNextRectangle(GetWordSize(wordWithCount.Key, fontSize)),
                _tagLayouterProperties.FontFamily);
        }
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
        var sizeF = _graphics.MeasureString(content, new Font(_tagLayouterProperties.FontFamily, fontSize));

        return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
}