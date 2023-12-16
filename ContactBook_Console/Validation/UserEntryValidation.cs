
using ContactBook_Console.Navigation;
using ContactBook_Console.Views;

namespace ContactBook_Console.Validation;

public class UserEntryValidation
{
    public string ValidateUserEntry(string userEntry)
    {
        bool valid = false;

        while (valid == false)
        {
            if (userEntry == "q")
            {
                valid = true;   
                return "q";
            }
            else if (userEntry == "")
            {
                Console.WriteLine("Error, please enter in a value");
                userEntry = Console.ReadLine()!; 
            }
            else { valid = true; }
        }

        return userEntry;
    }
}
