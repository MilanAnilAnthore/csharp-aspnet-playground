using BankAccount.Models;
using BankAccount.DataStorage;
using BankAccount.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankAccount.Services
{
    internal class TransactionMethods
    {
        public static void Deposit(Account account, List<Account> accountArray)
        {
            Console.Write("Enter the amount to deposit: ");
            
            if(decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    var newBalance = account.Deposit(amount);
                    account.Transactions.Add($"An amount of ${amount} has been deposited");
                    Console.WriteLine($"This is your new balance: ${newBalance}");

                    SaveAccounts(accountArray);
                }
                else
                {
                    Console.WriteLine("Enter a number greater than 0 to deposit");
                }

            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }

        public static void WithdrawAmount(Account account, List<Account> accountArray)
        {
            Console.Write("Enter the amount to withdraw: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Enter a number greater than 0 to withdraw");
                }
                else if (amount > account.Balance)
                {
                    Console.WriteLine($"You dont have enough balance to withdraw, current balance: ${account.Balance}");
                }
                else
                {
                    var newBalance = account.Withdraw(amount);

                    if (newBalance == -1)
                    {
                        Console.WriteLine($"Withdrawal failed. Insufficient balance: ${account.Balance}");
                    }
                    else
                    {
                        account.Transactions.Add($"An amount of ${amount} has been withdrawn, current Balance: ${account.Balance}");
                        Console.WriteLine($"Successfully withdrawn: ${amount}, current Balance: ${account.Balance}");

                        SaveAccounts(accountArray);
                    }
                }
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }

        public static void CheckBalance(Account account)
        {
            Console.WriteLine($"Current balance: ${account.Balance}");
        }

        public static void CheckTransactions(Account account)
        {
            if (account.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            foreach (var transaction in account.Transactions)
            {
                Console.WriteLine(transaction);
            }
        }

        private static void SaveAccounts(List<Account> accountArray)
        {
            var serializedAccountArray = JsonMethods.SerializeAccount(accountArray);
            File.WriteAllText(ConstantData.DataFilePath, serializedAccountArray);
        }
    }
}
