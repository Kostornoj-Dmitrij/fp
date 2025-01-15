namespace TagsCloudVisualization.Properties;

public class SaveProperties
{
    public string FilePath { get; }
    public string FileName { get; }
    public string FileFormat { get; }

    public SaveProperties(string filePath, string fileName, string fileFormat)
    {
        FilePath = filePath;
        FileName = fileName;
        FileFormat = fileFormat;
    }
}