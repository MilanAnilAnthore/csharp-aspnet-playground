using BankAccount.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Services
{
    internal class TransactionMethods
    {
        public static void Deposit(BankAccountClass account)
        {
            Console.Write("Enter the amount to deposit:");
            
            if(decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                   var newBalance = account.Deposit(amount);
                    account.Transactions.Add($"An amount of ${amount} has been deposited");
                    Console.WriteLine($"This is your new balance: ${newBalance}");
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

        public static void WithdrawAmount(BankAccountClass account)
        {
            Console.Write("Enter the amount to withdraw:");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount <= account.Balance)
                {
                    var newBalance = account.Withdraw(amount);
                    account.Transactions.Add($"An amount of ${amount} has been Withdrawn, current Balance: ${account.Balance}");
                    Console.WriteLine($"Successfully withdrawn: ${amount}, current Balance: ${account.Balance}");
                }
                else
                {
                    Console.WriteLine($"You dont have enough balance to withdraw, current balance: ${account.Balance}");
                }

            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }

        public static void CheckBalance(BankAccountClass account)
        {
            Console.WriteLine($"Current balance: ${account.Balance}");
        }

        public static void CheckTransactions(BankAccountClass account)
        {
            foreach (var transaction in account.Transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
