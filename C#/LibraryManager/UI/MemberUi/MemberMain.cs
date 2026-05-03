using System;
using System.Threading.Tasks;
using LibraryManager.Services;
using LibraryManager.Models.Members;

namespace LibraryManager.UI.MemberUi
{
    public class MemberMain
    {
        public static async Task MemberMainMenu(LibraryService service)
        {
            while (true)
            {
                Console.Write("""
                ── MEMBER MANAGEMENT ──

                1. Register Member
                2. Remove Member
                3. List All Members
                0. Back

                Select an option:
                """);

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 0 && choice <= 3)
                    {
                        switch (choice)
                        {
                            case 0:
                                {
                                    return;
                                }
                            case 1:
                                {
                                    Member member = AddMemberUi.AddMember();
                                    await service.RegisterMember(member);
                                    Console.WriteLine($"Member Registered successfully with ID: {member.memberID}");
                                    break;
                                }
                            case 2:
                                {
                                    while (true)
                                    {
                                        Console.Write("Enter Member ID to remove: ");
                                        string idInput = Console.ReadLine() ?? "";
                                        if (!string.IsNullOrWhiteSpace(idInput))
                                        {
                                            await service.RemoveMember(idInput);
                                            Console.WriteLine("Member removed successfully.");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter a valid Member ID");
                                        }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    var members = await service.ListAllMembers();
                                    Console.WriteLine("── ALL MEMBERS ──");
                                    foreach (var member in members)
                                    {
                                        Console.WriteLine($"ID: {member.memberID}, Name: {member.Name}");
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 0 and 3.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");
                }
            }
        }
    }
}