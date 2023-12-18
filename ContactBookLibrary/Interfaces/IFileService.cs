namespace ContactBookLibrary.Interfaces
{
    public interface IFileService
    {
        IServiceResult GetContentFromFile();
        IServiceResult SaveContentToFile(string content);
    }
}