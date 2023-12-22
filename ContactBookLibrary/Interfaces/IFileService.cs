namespace ContactBookLibrary.Interfaces
{

    /// <summary>
    /// Interface för FileServices
    /// </summary>
    public interface IFileService
    {
        IServiceResult GetContentFromFile(string filePath);
        IServiceResult SaveContentToFile(string content, string filePath);
    }
}