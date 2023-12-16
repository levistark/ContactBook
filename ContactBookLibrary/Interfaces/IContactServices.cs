using ContactBookLibrary.Models.Responses;

namespace ContactBookLibrary.Interfaces;

public interface IContactServices
{
    ServiceResult AddContact(IContact contact);
    ServiceResult DeleteContact(string email);
    ServiceResult GetContact(string email);
    ServiceResult ShowContacts();
    ServiceResult UpdateContact(string email);
}