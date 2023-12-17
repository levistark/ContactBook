using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using System.Diagnostics;

namespace ContactBookLibrary.Services;

public class ContactServices : IContactServices
{
    private List<IContact> _contacts = [];

    public ServiceResult AddContact(IContact contact)
    {
        try
        {
            if (_contacts.Any(existingContact => existingContact.Email == contact.Email))
                return new ServiceResult() { Status = Enums.ServiceStatus.ALREADY_EXISTS };

            if (contact == null)
                return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
            
            _contacts.Add(contact);
            return new ServiceResult() { Status = Enums.ServiceStatus.CREATED };    
        }

        catch (Exception ex) 
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED};
        }
    }

    public ServiceResult GetContacts()
    {
        try
        {
            if (_contacts.Any())
            {
                return new ServiceResult()
                {
                    Result = _contacts,
                    Status = Enums.ServiceStatus.SUCCESS
                };
            }
            else
                return new ServiceResult() { Status = Enums.ServiceStatus.NOT_FOUND };

        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public ServiceResult GetContact(string contactListIndex)
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
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public ServiceResult UpdateContact(IContact contact)
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
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public ServiceResult DeleteContact(IContact contact)
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
