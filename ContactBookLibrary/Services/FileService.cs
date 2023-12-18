using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using System.Diagnostics;

namespace ContactBookLibrary.Services;

public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    public IServiceResult GetContentFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                return new ServiceResult() { Status = Enums.ServiceStatus.SUCCESS, Result = sr.ReadToEnd() };
            }

            return new ServiceResult() { Status = Enums.ServiceStatus.NOT_FOUND };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public IServiceResult SaveContentToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }

            return new ServiceResult() { Status = Enums.ServiceStatus.CREATED };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }

    }
}
