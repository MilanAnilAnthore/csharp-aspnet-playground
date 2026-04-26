using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.ConsoleUI
{
    internal class UserPrompts
    {

        public static int InitialPrompt()
        {
            Console.WriteLine("Welcome to the Bank Account Creation System!");
            Console.WriteLine("Please Choose one of the following options to continue");
            while (true)
            {
                Console.WriteLine("1. Create a new account");
                Console.WriteLine("2. Login to existing account");
                Console.WriteLine("3. Exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if(choice == 1 || choice == 2 || choice == 3)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2 or 3 to exit");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number (1 or 2) or 3 to exit");
                }
            }
        }


        public static int OptionsPrompt()
        {
            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Check Transaction History");
            Console.WriteLine("5. Exit");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 1 && choice <= 5)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                }
            }
        }

        public static string GetUserName()
        {
            Console.Write("ENTER ACCOUNT HOLDER NAME: ");
            string? userName = Console.ReadLine();

            while (int.TryParse(userName, out int userNum) || string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty or number Please try again.");
                Console.Write("Enter your username: ");
                userName = Console.ReadLine();
            }

            userName = userName.ToLower();
            return userName;
        }


        public static decimal GetInitialBalance()
        {
            while (true)
            {
                Console.Write("ENTER INITIAL BALANCE: ");
                string? input = Console.ReadLine();


                if (decimal.TryParse(input, out decimal initialBalance))
                {
                    if (initialBalance < 0)
                    {
                        Console.WriteLine("Balance cannot be negative. Please enter a valid amount.");
                        continue;
                    }
                    else
                    {
                        return initialBalance;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number for the balance.");
                }
            }
        }
    }
}
