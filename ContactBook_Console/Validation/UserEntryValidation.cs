
using ContactBook_Console.Navigation;
using ContactBook_Console.Views;

namespace ContactBook_Console.Validation;

public class UserEntryValidation
{

    /// <summary>
    /// ValidateUserEntry är en metod som kontrollerar om användarens text-input är lika med 'q' eller en tom sträng. 
    /// Den loopar igenom userEntry tills valideringen godkänns och returnerar sedan tillbaka textsträngen userEntry.
    /// </summary>

    public string ValidateUserEntry(string userEntry)
    {
        bool valid = false;

        while (valid == false)
        {
            if (userEntry == "")
            {
                Console.WriteLine("Error, please enter in a value");
                userEntry = Console.ReadLine()!; 
            }
            else { valid = true; }
        }
        return userEntry;
    }
}
