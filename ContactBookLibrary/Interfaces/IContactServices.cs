using ContactBookLibrary.Models.Responses;

namespace ContactBookLibrary.Interfaces;

public interface IContactServices
{
    ServiceResult AddContact(IContact contact);
    ServiceResult DeleteContact(IContact contact);
    ServiceResult GetContact(string email);
    ServiceResult GetContacts();
    ServiceResult UpdateContact(IContact contact);
}