using ContactBookLibrary.Models.Responses;

namespace ContactBookLibrary.Interfaces;

public interface IContactServices
{
    IServiceResult AddContact(IContact contact);
    IServiceResult DeleteContact(IContact contact);
    IServiceResult GetContact(string email);
    IServiceResult GetContacts();
    IServiceResult UpdateContact(IContact contact);
}