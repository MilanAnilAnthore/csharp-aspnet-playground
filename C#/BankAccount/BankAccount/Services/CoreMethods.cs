using BankAccount.ConsoleUI;
using BankAccount.DataStorage;
using BankAccount.Constants;
using BankAccount.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankAccount.Services
{
    internal class CoreMethods
    {
        public static void CreateAccount(List<BankAccountClass> accountArray)
        {
            string userName = UserPrompts.GetUserName();
            decimal initialBalance = UserPrompts.GetInitialBalance();

            BankAccountClass account = new BankAccountClass()
            {
                AccountHolder = userName,
                Balance = initialBalance
            };

            accountArray.Add(account);

            // Save the updated list whenever a new account is added
            var serializedAccountArray = JsonMethods.SerializeAccount(accountArray);
            File.WriteAllText(ConstantData.DataFilePath, serializedAccountArray);

            Console.WriteLine($"Account created successfully for {account.AccountHolder} with initial balance of ${account.Balance}");
        }

        public static void LoginAccount(List<BankAccountClass> accountArray)
        {
            while (true)
            {
                Console.Write("Enter the account Holders name (or type 'cancel' to exit): ");
                var name = Console.ReadLine()?.ToLower();

                if (name == "cancel")
                    break;

                var foundAccount = accountArray.FirstOrDefault(a => a.AccountHolder?.ToLower() == name);

                if (foundAccount != null)
                {
                    Console.WriteLine($"Account found for {foundAccount.AccountHolder} with balance of ${foundAccount.Balance}");
                    break;
                }
                else
                {
                    Console.WriteLine($"No account found for '{name}'. Please try again.");
                }
            }
        }
    }
}
