using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Models
{
    internal class Account
    {
        public string AccountHolder { get; set; } = "";

        private decimal balance;
        public decimal Balance
        {
            get { return balance; }
            set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Balance cannot be negative.");
                }

                balance = value;
            }
        }

        public decimal Deposit(decimal amount)
        {
            Balance += amount;
            return Balance;
        }

        public decimal Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                return -1; // Insufficient funds
            }

            Balance -= amount;
            return Balance;
        }

        public List<string> Transactions { get; set; } = new List<string>();
    }
}
