using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models.Responses;
using ContactBookLibrary.Services;
using MauiContactBook.Interfaces;
using Contact = ContactBookLibrary.Models.Contact;

namespace MauiContactBook.Services;

public class MauiContactServices : IMauiContactServices
{
    private readonly ContactServices _contactServices;

    public MauiContactServices(ContactServices contactServices)
    {
        _contactServices = contactServices;
    }

    public event EventHandler? ContactsUpdated;

    /// <summary>
    /// En metod som lägger till en kontakt i listan via ContactServices
    /// </summary>
    /// <param name="contact">Contact Model</param>
    /// <returns>ServiceStatus</returns>
    public IServiceResult AddContactToList(Contact contact)
    {
        var result = _contactServices.AddContact(contact);
        ContactsUpdated?.Invoke(this, EventArgs.Empty);
        return HandleServiceResult(result);
    }

    /// <summary>
    /// En metod som uppdaterar en kontakt i listan via ContactServices
    /// </summary>
    /// <param name="contact">Contact Model</param>
    /// <returns>ServiceStatus</returns>
    public IServiceResult UpdateContact(Contact contact)
    {
        var result = _contactServices.UpdateContactFully(contact);
        ContactsUpdated?.Invoke(this, EventArgs.Empty);
        return HandleServiceResult(result);
    }

    /// <summary>
    /// En metod som tar bort en kontakt ur listan via ContactServices
    /// </summary>
    /// <param name="result">Contact Model</param>
    /// <returns>SericeStatus</returns>
    public IServiceResult RemoveContactFromList(Contact contact)
    {
        var result = _contactServices.DeleteContact(contact);
        ContactsUpdated?.Invoke(this, EventArgs.Empty);
        return HandleServiceResult(result);
    }

    /// <summary>
    /// En metod som hämtar kontakter från listan i ContactServices
    /// </summary>
    /// <returns>IEnumerable</returns>
    public IEnumerable<IContact> GetContactsFromList()
    {
        var contactList = _contactServices.GetContacts().Result as IEnumerable<IContact>;

        if (contactList != null)
            return contactList;
        else
            return Enumerable.Empty<IContact>();
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
