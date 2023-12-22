using ContactBookLibrary.Models;
using ContactBookLibrary.Models.Responses;

namespace ContactBookLibrary.Interfaces;

/// <summary>
/// Detta är ContactServices-interfacet
/// </summary>
public interface IContactServices
{
    IServiceResult AddContact(Contact contact);
    IServiceResult DeleteContact(Contact contact);
    IServiceResult GetContact(string email);
    IServiceResult GetContacts();
    IServiceResult UpdateContact(Contact contactToBeUpdated, string contactListIndex, string propertyIndex);
}