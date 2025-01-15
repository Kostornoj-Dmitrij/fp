using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Readers;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class ReadersShould
{
    [Test]
    public void ReadText_TxtFile_ReturnWordsInFile()
    {
        var reader = new TxtReader();

        var words = reader.Read("Files/txtFile.txt");
        //На данном этапе мы не удаляем стоп слова, а wordsGetter стал считывать не только буквы,
        //поэтому пришлось "++" и "=" добавить сюда
        words.Should().BeEquivalentTo(["Солнце", "светило", "Ярко", "++", "Наступил", "=", "ДЕНЬ"]);
    }

    [Test]
    public void ReadText_DocFile_ReturnWordsInFile()
    {
        var reader = new DocReader();

        var words = reader.Read("Files/docFile.doc");

        words.Should().BeEquivalentTo(["Солнце", "светило", "Ярко", "++", "Наступил", "=", "ДЕНЬ"]);
    }

    [Test]
    public void ReadText_DocxFile_ReturnWordsInFile()
    {
        var reader = new DocxReader();

        var words = reader.Read("Files/docxFile.docx");

        words.Should().BeEquivalentTo(["Солнце", "светило", "Ярко", "++", "Наступил", "=", "ДЕНЬ"]);
    }
}