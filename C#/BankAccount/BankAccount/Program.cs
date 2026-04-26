using BankAccount.ConsoleUI;
using BankAccount.Constants;
using BankAccount.DataStorage;
using BankAccount.Services;

class Program
{
    static void Main()
    {

        // 1. Load accounts at the start
        var accountArray = JsonMethods.LoadAccounts(ConstantData.DataFilePath);

        bool isRunning = true;

        // 2. Main menu loop
        while (isRunning)
        {
            int choice = UserPrompts.InitialPrompt();

            // 3. Switch statement for cleaner routing
            switch (choice)
            {
                case 1:
                    CoreMethods.CreateAccount(accountArray);
                    break;

                case 2:
                    CoreMethods.LoginAccount(accountArray);
                    break;

                case 3:
                    Console.WriteLine("Thank you for using the bank! Goodbye.");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}