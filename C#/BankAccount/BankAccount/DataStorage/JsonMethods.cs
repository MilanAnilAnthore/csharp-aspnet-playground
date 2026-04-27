using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using BankAccount.Models;


namespace BankAccount.DataStorage
{
    internal class JsonMethods
    {
        public static string SerializeAccount(List<Account> account)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(account, options);
        }

        public static List<Account> DeserializeAccount(string jsonData)
        {
            return JsonSerializer.Deserialize<List<Account>>(jsonData) ?? new List<Account>();
        }

        public static List<Account> LoadAccounts(string DataFilePath)
        {
            if (File.Exists(DataFilePath))
            {
                var jsonData = File.ReadAllText(DataFilePath);
                return JsonMethods.DeserializeAccount(jsonData) ?? new List<Account>();
            }
            return new List<Account>();
        }
    }
}

