using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using System.Diagnostics;

namespace ContactBookLibrary.Services;

/// <summary>
/// Här hanteras alla tjänster som har med att ändra eller läsa filen som innehåller kontakt-listan
/// </summary>
public class FileService : IFileService
{
    /// <summary>
    /// Metod som läser av en fil och returnerar dess information
    /// </summary>
    /// <param name="filePath">File path til filen som ska ändras/läsas</param>
    public IServiceResult GetContentFromFile(string filePath)
    {
        try
        {
            // Kontrollera om filen existerar på angiven file path
            if (File.Exists(filePath))
            {
                // Skapa StreamReader för att läsa av och returnera filens info
                using var sr = new StreamReader(filePath);
                return new ServiceResult() { Status = Enums.ServiceStatus.SUCCESS, Result = sr.ReadToEnd() };
            }

            // Om inte filen hittas på angiven file path
            return new ServiceResult() { Status = Enums.ServiceStatus.NOT_FOUND };
        }

        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    /// <summary>
    /// En metod som sparar ner information till fil
    /// </summary>
    /// <param name="content">Information som ska skrivas till fil</param>
    /// <param name="filePath">File path till filen som ska uppdateras</param>
    public IServiceResult SaveContentToFile(string content, string filePath)
    {
        try
        {
            // Skriv ner content till fil via filePath med hjälp av StreamWriter
            using (var sw = new StreamWriter(filePath))
            {
                sw.WriteLine(content);
            }

            return new ServiceResult() { Status = Enums.ServiceStatus.UPDATED };
        }

        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }
}
