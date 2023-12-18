using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ContactBookLibrary.Services;

public class ContactServices : IContactServices
{
    private List<IContact> _contacts = [];

    FileService _fileService = new FileService(@"C:\VSProjects\content.json");

    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Objects
    };

    public IServiceResult AddContact(IContact contact)
    {
        try
        {
            if (_contacts.Any(existingContact => existingContact.Email == contact.Email))
                return new ServiceResult() { Status = Enums.ServiceStatus.ALREADY_EXISTS };

            if (contact == null)
                return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
            
            _contacts.Add(contact);

            var fileSaveResult = _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts, jsonSerializerSettings));

            if (fileSaveResult.Status == Enums.ServiceStatus.CREATED)
                return new ServiceResult() { Status = Enums.ServiceStatus.CREATED };
            else
                return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }

        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED};
        }
    }

    public IServiceResult GetContacts()
    {
        try
        {
            var content = _fileService.GetContentFromFile();

            if (content.Result != null)
            {
                if (content.Result is string json)
                _contacts = JsonConvert.DeserializeObject<List<IContact>>(json, jsonSerializerSettings)!;

                return new ServiceResult()
                {
                    Result = _contacts,
                    Status = Enums.ServiceStatus.SUCCESS
                };
            }

            return new ServiceResult() { Status = Enums.ServiceStatus.NOT_FOUND };


            //if (_contacts.Any())
            //{
            //    return new ServiceResult()
            //    {
            //        Result = _contacts,
            //        Status = Enums.ServiceStatus.SUCCESS
            //    };
            //}
            //else
            //    return new ServiceResult() { Status = Enums.ServiceStatus.NOT_FOUND };

        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public IServiceResult GetContact(string contactListIndex)
    {
        try
        {
            // Try to convert string to int using int.TryParse
            if (int.TryParse(contactListIndex, out int index))
            {
                return new ServiceResult() 
                { 
                    Result = _contacts[index],
                    Status = Enums.ServiceStatus.SUCCESS
                };
            }
            else
            {
                Console.WriteLine("The user entry is not a valid integer.");
                return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };

            }
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public IServiceResult UpdateContact(IContact contact)
    {
        try
        {
            var existingContact = _contacts.Find(c => c.Id == contact.Id);

            if (existingContact != null)
            {
                // Update the existing contact
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                existingContact.Phone = contact.Phone;
                existingContact.Address = contact.Address;

                return new ServiceResult() { Status = Enums.ServiceStatus.UPDATED };
            }
            else
            {
                // Contact not found
                return new ServiceResult() { Status = Enums.ServiceStatus.NOT_FOUND };
            }
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public IServiceResult DeleteContact(IContact contact)
    {
        try
        {
            _contacts.Remove(contact);
            return new ServiceResult() { Status = Enums.ServiceStatus.DELETED };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }


}
