
using ContactBookLibrary.Services;

namespace ContactBook_Console.Navigation;

public class MenuNavigation
{
    private Dictionary<string, Action> menuOptions = new Dictionary<string, Action>();
     
    public void AddMenuOption(string option, Action action)
    {
        menuOptions[option] = action;
    }

    public void MenuValidation()
    {
        var userEntry = Console.ReadLine();
        bool valid = false;

        while (!valid)
        {
            if (menuOptions.ContainsKey(userEntry!))
            {
                // Execute the corresponding action for the selected menu option
                menuOptions[userEntry!].Invoke();
                valid = true;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again:");
                userEntry = Console.ReadLine();
            }
        }
    }
}
