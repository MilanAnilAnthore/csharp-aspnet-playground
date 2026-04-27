using BankAccount.ConsoleUI;
using BankAccount.DataStorage;
using BankAccount.Constants;
using BankAccount.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankAccount.Services
{
    internal class CoreMethods
    {
        public static void CreateAccount(List<Account> accountArray)
        {
            string userName = UserPrompts.GetUserName();

            // Check for duplicate account name
            var existingAccount = accountArray.FirstOrDefault(a => a.AccountHolder?.ToLower() == userName);
            if (existingAccount != null)
            {
                Console.WriteLine($"An account with the name '{userName}' already exists. Please use a different name.");
                return;
            }

            decimal initialBalance = UserPrompts.GetInitialBalance();

            Account account = new Account()
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

        public static void LoginAccount(List<Account> accountArray)
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

                    // Loop so the user can perform multiple operations per login
                    bool loggedIn = true;
                    while (loggedIn)
                    {
                        var result = UserPrompts.OptionsPrompt();

                        switch (result)
                        {
                            case 1:
                                TransactionMethods.Deposit(foundAccount, accountArray);
                                break;
                            case 2:
                                TransactionMethods.WithdrawAmount(foundAccount, accountArray);
                                break;
                            case 3:
                                TransactionMethods.CheckBalance(foundAccount);
                                break;
                            case 4:
                                TransactionMethods.CheckTransactions(foundAccount);
                                break;
                            case 5:
                                Console.WriteLine("Logging out...");
                                loggedIn = false;
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }
                    }
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
