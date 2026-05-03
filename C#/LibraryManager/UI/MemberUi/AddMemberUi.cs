using System;
using LibraryManager.Models.Members;

namespace LibraryManager.UI.MemberUi
{
    public class AddMemberUi
    {
        public static Member AddMember()
        {
            while (true)
            {
                Console.WriteLine("── REGISTER MEMBER ─-");
                Console.Write("Enter Name: ");
                string Name = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    return new Member { memberID = Guid.NewGuid(), Name = Name };
                }
                else
                {
                    Console.WriteLine("Name cannot be empty.");
                }
            }
        }
    }
}
