using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models;
using ContactBookLibrary.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ContactBookLibrary.Services;

public class ContactServices : IContactServices
{
    List<IContact> _contacts = [];

    private string filePath = @"C:\VSProjects\content.json";

    private IFileService _fileService;

    public ContactServices(IFileService fileService)
    {
        _fileService = fileService;
    }

    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };

    public IServiceResult AddContact(Contact contact)
    {
        try
        {
            if (_contacts.Any(existingContact => existingContact.Email == contact.Email))
                return new ServiceResult() { Status = ServiceStatus.ALREADY_EXISTS };

            if (contact == null)
                return new ServiceResult() { Status = ServiceStatus.FAILED };
            
            _contacts.Add(contact);

            var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

            if (fileSaveResult.Status == ServiceStatus.UPDATED)
                return new ServiceResult() { Status = ServiceStatus.CREATED };
            else
                return new ServiceResult() { Status = ServiceStatus.FAILED };
        }

        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED};
        }
    } 

    public IServiceResult GetContacts()
    {
        try
        {
            var content = _fileService.GetContentFromFile(filePath);

            if (content.Result != null)
            {
                if (content.Result is string json)
                    _contacts = JsonConvert.DeserializeObject<List<IContact>>(json, jsonSerializerSettings)!;

                return new ServiceResult()
                {
                    Result = _contacts,
                    Status = ServiceStatus.SUCCESS
                };
            }

            return new ServiceResult() { Status = ServiceStatus.NOT_FOUND };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    public IServiceResult GetContact(string contactListIndex)
    {
        try
        {
            if (int.TryParse(contactListIndex, out int index))
            {
                return new ServiceResult() 
                { 
                    Result = _contacts[index],
                    Status = ServiceStatus.SUCCESS
                };
            }
            else
            {
                Console.WriteLine("The user entry is not a valid integer.");
                return new ServiceResult() { Status = ServiceStatus.FAILED };
            }
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    public IServiceResult UpdateContact(Contact contactToBeUpdated, string newValue, string propertyToChange)
    {
        try
        {
            // Find the contact to update in list
            var existingContact = _contacts.Find(c => c.Id == contactToBeUpdated.Id);

            if (existingContact != null)
            {
                //Choose which property to change
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

                // Update the file with new list data
                var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

                // Hanlde service results
                return HandleServiceResult(fileSaveResult);
            }

            else
            {
                // Contact IDs did not match
                return new ServiceResult() { Status = ServiceStatus.NOT_FOUND };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

    public IServiceResult DeleteContact(Contact contact)
    {
        try
        {
            // Remove contact from contact list
            _contacts.Remove(contact);

            // Update the file with new list data
            var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings), filePath);

            // Hanlde service results
            return HandleServiceResult(fileSaveResult);
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = ServiceStatus.FAILED };
        }
    }

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
