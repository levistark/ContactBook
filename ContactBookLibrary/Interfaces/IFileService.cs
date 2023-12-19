namespace ContactBookLibrary.Interfaces
{
    public interface IFileService
    {
        IServiceResult GetContentFromFile(string filePath);
        IServiceResult SaveContentToFile(string content, string filePath);
    }
}