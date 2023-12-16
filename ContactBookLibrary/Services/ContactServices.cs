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

    public ServiceResult ShowContacts()
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

    public ServiceResult GetContact(string email)
    {
        try
        {
            return new ServiceResult() { };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public ServiceResult UpdateContact(string email)
    {
        try
        {
            return new ServiceResult() { };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }

    public ServiceResult DeleteContact(string email)
    {
        try
        {
            return new ServiceResult() { };
        }

        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResult() { Status = Enums.ServiceStatus.FAILED };
        }
    }


}
