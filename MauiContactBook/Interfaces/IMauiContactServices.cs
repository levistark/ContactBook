using ContactBookLibrary.Interfaces;

namespace MauiContactBook.Interfaces
{
    public interface IMauiContactServices
    {
        event EventHandler? ContactsUpdated;
        IServiceResult AddContactToList(ContactBookLibrary.Models.Contact contact);
        IEnumerable<IContact> GetContactsFromList();
        IServiceResult HandleServiceResult(IServiceResult result);
        IServiceResult RemoveContactFromList(ContactBookLibrary.Models.Contact contact);
        IServiceResult UpdateContact(ContactBookLibrary.Models.Contact contact);
    }
}