
using ContactBookLibrary.Services;

namespace ContactBook_Console.Navigation;

public class MenuNavigation
{
    /// <summary>
    /// Skapar ett Dictionary som är till för att para ihop en textsträng och en metod i form av en Action
    /// </summary>
    private Dictionary<string, Action> menuOptions = new Dictionary<string, Action>();
    
    /// <summary>
    /// AddMenuOption är en metod som tar emot ett strängvärde som ska paras ihop med en metod som representarar en viss vy
    /// </summary>
    public void AddMenuOption(string option, Action action)
    {
        menuOptions[option] = action;
    }

    /// <summary>
    /// MenuValidation är en metod som är menad att användas i samband med AddMenuOption och är till för att validera användarens input (userEntry). 
    /// Den kontrollerar om userEntry finns i Dictionaryt menuOptions, och triggar metoden som matchas med userEntry, eller loggar ut ett felmeddelande 
    /// </summary>
    public void MenuValidation()
    {
        var userEntry = Console.ReadLine();
        bool valid = false;

        while (!valid)
        {
            if (menuOptions.ContainsKey(userEntry!))
            {
                // Invoka metoden i menuOptions som matchas med userEntry
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
