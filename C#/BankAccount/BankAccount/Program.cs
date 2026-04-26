class BankAccountClass
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

    public List<string> Transactions { get; } = new List<string>();
}

class Program
{
    static void Main()
    {
        string userName = BankAccount.AccountCreation.GetUserName();
        decimal initialBalance = BankAccount.AccountCreation.GetInitialBalance();

        BankAccountClass account = new BankAccountClass
        {
            AccountHolder = userName,
            Balance = initialBalance
        };
    }
}