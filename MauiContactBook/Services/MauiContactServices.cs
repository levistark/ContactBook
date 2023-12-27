using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using Contact = ContactBookLibrary.Models.Contact;


namespace MauiContactBook.Services;

public class MauiContactServices
{
    private readonly ContactServices _contactServices;

    public MauiContactServices(ContactServices contactServices)
    {
        _contactServices = contactServices; 
    }

    //private List<Contact> _contacts = [];

    //public List<Contact> GetContacts()
    //{
    //    var result = _contactServices.GetContacts().Result;
    //    _contacts = result as List<Contact> ?? null!;

    //    return result as List<Contact> ?? null!;
    //}
}
