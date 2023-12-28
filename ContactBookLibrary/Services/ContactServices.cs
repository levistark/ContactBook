using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models;
using ContactBookLibrary.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ContactBookLibrary.Services;

/// <summary>
/// ContactServices innehåller alla metoder som läser, eller ändrar kontakt-listan _contacts och påkallar metoder från FileServices. 
/// </summary>

public class ContactServices : IContactServices
{
    // Kontaktlistan
    List<IContact> _contacts = [];

    // Filepath som används vid påkallning av FileService-metoder
    private readonly string filePath = @"C:\VSProjects\ContactBook\content.json";

    // DI av FileServices
    private IFileService _fileService;

    public ContactServices(IFileService fileService)
    {
        _fileService = fileService;
    }

    // Sätter inställningarna för JSON-konverteringar
    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };

    /// <summary>
    /// Metod som lägger till kontakter i _contacts, samt sparar ner ändringar till fil via FileServices
    /// </summary>

    public IServiceResult AddContact(Contact contact)
    {
        try
        {

            // Kontrollerar om kontakt är null
            if (contact == null)
                return new ServiceResult() { Status = ServiceStatus.FAILED };

            // Kontrollerar om kontakten redan finns i listan
            if (_contacts.Any(existingContact => existingContact.Email == contact.Email))
                return new ServiceResult() { Status = ServiceStatus.ALREADY_EXISTS };

            // Lägger till kontakt i listan
            _contacts.Add(contact);

            // Sparar ner kontakt till fil via FileServices, samt konverterar om den uppdaterade listan _contacts till JSON-format
            var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

            // Hanterar ServiceResults
            if (fileSaveResult.Status == ServiceStatus.UPDATED)
                return new ServiceResult() { Status = ServiceStatus.CREATED };
            else
                return new ServiceResult() { Status = ServiceStatus.FAILED };
        }

        // Hantera exceptions
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED};
        }
    } 

    /// <summary>
    /// Metod som läser och returnerar befintliga kontakter från .json filen via FileServices
    /// </summary>
    public IServiceResult GetContacts()
    {
        try
        {
            // Hämta upp JSON-innehållet från filen via FileServices
            var content = _fileService.GetContentFromFile(filePath);

            // Kontrollera ServiceResult
            if (content.Result != null)
            {
                // Kontrollera resultatet och gör om _contacts till resultatet som ska konverteras till en lista av IContact
                if (content.Result is string json)
                    _contacts = JsonConvert.DeserializeObject<List<IContact>>(json, jsonSerializerSettings)!;

                // Returnera listan och statuskod
                return new ServiceResult()
                {
                    Result = _contacts,
                    Status = ServiceStatus.SUCCESS
                };
            }
            // Om inte filen hittas från FileServices, till exempel om filePath är inkorrekt.
            return new ServiceResult() { Status = ServiceStatus.NOT_FOUND };
        }

        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    /// <summary>
    /// Metod som hämtar upp en enskild kontakt från listan _contacts via dess index i listan
    /// </summary>

    public IServiceResult GetContact(string contactListIndex)
    {
        try
        {
            // Konvertera om string till int
            if (int.TryParse(contactListIndex, out int index))
            {
                // Returnera IContact vid index från _contacts
                return new ServiceResult() 
                { 
                    Result = _contacts[index],
                    Status = ServiceStatus.SUCCESS
                };
            }

            // Om inte string kan konverteras till int 
            else
            {
                Console.WriteLine("The user entry is not a valid integer.");
                return new ServiceResult() { Status = ServiceStatus.FAILED };
            }
        }

        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    /// <summary>
    /// Metod som uppdaterar en kontakt i listan, samt sparar ner ändringar till fil via FileService
    /// </summary>
    /// <param name="contactToBeUpdated">Kontakt-objektet som ska uppdateras</param>
    /// <param name="newValue">Det nya värdet</param>
    /// <param name="propertyToChange">Vald property som ska uppdateras</param>

    public IServiceResult UpdateContact(Contact contactToBeUpdated, string newValue, string propertyToChange)
    {
        try
        {
            // Hitta kontakten som ska uppdateras i _contacts, baserat på dess Guid
            var existingContact = _contacts.Find(c => c.Id == contactToBeUpdated.Id);

            // Kontrollera resultatet
            if (existingContact != null)
            {
                // Kontrollera vilken property som ska uppdateras
                switch (propertyToChange)
                {
                    case "1":
                        existingContact.FirstName = newValue;
                        break;
                    case "2":
                        existingContact.LastName = newValue;
                        break;
                    case "3":
                        existingContact.Email = newValue;
                        break;
                    case "4":
                        existingContact.Phone = newValue;
                        break;
                    case "5":
                        existingContact.Address = newValue;
                        break;
                }

                // Uppdatera filen med  uppdaterad information från _contacts
                var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

                // Hantera ServiceResult
                return HandleServiceResult(fileSaveResult);
            }

            else
            {
                // Om inte kontakten hittas i listan
                return new ServiceResult() { Status = ServiceStatus.NOT_FOUND };
            }
        }
        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    /// <summary>
    /// Metod som uppdaterar en kontakt i listan, samt sparar ner ändringar till fil via FileService
    /// </summary>
    /// <param name="contactToBeUpdated">Kontakt-objektet som ska uppdateras</param>
    /// <param name="newValue">Det nya värdet</param>
    /// <param name="propertyToChange">Vald property som ska uppdateras</param>

    public IServiceResult UpdateContactFully(Contact newContactObject)
    {
        try
        {
            // Hitta kontakten som ska uppdateras i _contacts, baserat på dess Guid
            var existingContact = _contacts.Find(c => c.Id == newContactObject.Id);

            // Kontrollera resultatet
            if (existingContact != null)
            {
                if (newContactObject.FirstName != "")
                    existingContact.FirstName = newContactObject.FirstName;
                
                if (newContactObject.LastName != "")
                    existingContact.LastName = newContactObject.LastName;

                if (newContactObject.Email != "")
                    existingContact.Email = newContactObject.Email;
                
                if (newContactObject.Phone != "")
                    existingContact.Phone= newContactObject.Phone;
               
                if (newContactObject.Address != "")
                    existingContact.Address = newContactObject.Address;

                // Uppdatera filen med  uppdaterad information från _contacts
                var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

                // Hantera ServiceResult
                return HandleServiceResult(fileSaveResult);
            }

            else
            {
                // Om inte kontakten hittas i listan
                return new ServiceResult() { Status = ServiceStatus.NOT_FOUND };
            }
        }
        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    /// <summary>
    /// Metod som tar bort en kontakt från listan och uppdaterar fil via FileService
    /// </summary>

    public IServiceResult DeleteContact(Contact contact)
    {
        try
        {
            // Hitta kontakten som ska uppdateras i _contacts, baserat på dess Guid
            var existingContact = _contacts.Find(c => c.Id == contact.Id); 
            
            if (existingContact != null)
            {
                // Ta bort kontakt från _contacts
                _contacts.Remove(existingContact);
            }

            // Uppdatera filen med  uppdaterad information från _contacts
            var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

            // Hantera ServiceResult
            return HandleServiceResult(fileSaveResult);
        }
        // Hantera exceptions
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    /// <summary>
    /// Metod som hanterar ServiceResults
    /// </summary>
    public IServiceResult HandleServiceResult(IServiceResult result)
    {
        switch (result.Status)
        {
            case ServiceStatus.SUCCESS:
                return new ServiceResult() { Status = ServiceStatus.SUCCESS };

            case ServiceStatus.CREATED:
                return new ServiceResult() { Status = ServiceStatus.CREATED };

            case ServiceStatus.UPDATED:
                return new ServiceResult() { Status = ServiceStatus.UPDATED };

            case ServiceStatus.FAILED:
                return new ServiceResult() { Status = ServiceStatus.FAILED };

            case ServiceStatus.NOT_FOUND:
                return new ServiceResult() { Status = ServiceStatus.NOT_FOUND };

            case ServiceStatus.ALREADY_EXISTS:
                return new ServiceResult() { Status = ServiceStatus.ALREADY_EXISTS };

            default:
                return new ServiceResult() { Status = ServiceStatus.DELETED };
        }
    }
}
