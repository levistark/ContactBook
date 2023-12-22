namespace ContactBookLibrary.Interfaces;

/// <summary>
/// Detta är kontakt-interfacet
/// </summary>
/// 
public interface IContact
{
    string Address { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    Guid Id { get; set; }
    string LastName { get; set; }
    string Phone { get; set; }
}