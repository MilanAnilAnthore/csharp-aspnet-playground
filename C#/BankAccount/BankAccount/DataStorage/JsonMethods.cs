using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using BankAccount.Classes;


namespace BankAccount.DataStorage
{
    internal class JsonMethods
    {
        public static string SerializeAccount(List<BankAccountClass> account)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(account, options);
        }

        public static List<BankAccountClass> DeserializeAccount(string jsonData)
        {
            return JsonSerializer.Deserialize<List<BankAccountClass>>(jsonData) ?? new List<BankAccountClass>();
        }

        public static List<BankAccountClass> LoadAccounts(string DataFilePath)
        {
            if (File.Exists(DataFilePath))
            {
                var jsonData = File.ReadAllText(DataFilePath);
                return JsonMethods.DeserializeAccount(jsonData) ?? new List<BankAccountClass>();
            }
            return new List<BankAccountClass>();
        }
    }
}
