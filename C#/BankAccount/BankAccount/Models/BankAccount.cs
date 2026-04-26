using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Classes
{
    internal class BankAccountClass
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

        public List<string> Transactions { get; set; } = new List<string>();
    }
}
