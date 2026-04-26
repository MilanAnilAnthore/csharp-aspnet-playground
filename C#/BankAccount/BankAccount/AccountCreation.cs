using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    internal class AccountCreation
    {
        public static string GetUserName()
        {
            Console.Write("ENTER ACCOUNT HOLDER NAME: ");
            string? userName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Username cannot be empty. Please try again.");
                Console.Write("Enter your username: ");
                userName = Console.ReadLine();
            }
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
