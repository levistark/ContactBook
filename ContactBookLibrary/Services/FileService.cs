using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using System.Diagnostics;

namespace ContactBookLibrary.Services;

public class FileService() : IFileService
{

    public IServiceResult GetContentFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using var sr = new StreamReader(filePath);
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

    public IServiceResult SaveContentToFile(string content, string filePath)
    {
        try
        {
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(content);
            }

            return new ServiceResult() { Status = Enums.ServiceStatus.UPDATED };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }
}
