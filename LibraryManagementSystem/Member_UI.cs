using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Member_UI
    {
        public static void MemberLogin()
        {
            Console.WriteLine(new string('-', 83));
            Console.WriteLine("Member Login");
            Console.Write("Enter Member ID: ");
            string memberId = Console.ReadLine();
            Console.Write("Enter Member Password: ");
            string memberPassword = Console.ReadLine();

            // Validate member login
            if (ValidateMemberLogin(memberId, memberPassword))
            {

                Member loggedInMember = Library.Members.Find(m => m.MembershipID == memberId);
                if (loggedInMember != null)
                {
                    Console.WriteLine($"\nWelcome, {loggedInMember.Name}!\n");
                    MemberMenu(loggedInMember);
                    Console.WriteLine(new string('-', 83));
                }
                else
                {
                    Console.WriteLine("Member not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Member ID or Password.");
                Program.ShowUserOptions();
            }
        }

        public static bool ValidateMemberLogin(string memID, string memPwd)
        {


            List<Member> members = Library.Members;


            Member loggedInMember = members.Find(member => member.MembershipID == memID);

            if (loggedInMember != null)
            {
                return loggedInMember.Password == memPwd;
            }

            // If no member with the provided ID is found, return false
            return false;
        }

        public static void MemberMenu(Member member)
        {
            while (true)
            {
                Console.WriteLine(new string('-', 83));
                Console.WriteLine($"\nMember Menu - {member.Name}");
                Console.WriteLine(new string('-', 83));
                Console.WriteLine("1. Borrow a Book");
                Console.WriteLine("2. Return a Book");
                Console.WriteLine("3. View Borrowed Books");
                Console.WriteLine("4. View Available Books");
                Console.WriteLine("5. Change Member Password");
                Console.WriteLine("6. View Member Details");
                Console.WriteLine("7. View Penalty Details");
                Console.WriteLine("8. Logout");

                Console.Write("Enter your choice: ");
                try
                {
                    string choice = Console.ReadLine();
                    int numChoice = int.Parse(choice);
                    ProcessMemberMenuChoice(numChoice, member);
                    Console.WriteLine(new string('-', 83));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a numeric option.");
                    Console.WriteLine(new string('-', 83));
                }

            }
        }

        private static void ProcessMemberMenuChoice(int choice, Member member)
        {
            switch (choice)
            {
                case 1:
                    BorrowBookMenu(member);
                    break;
                case 2:
                    ReturnBookMenu(member);
                    break;
                case 3:
                    member.LoanList();
                    break;
                case 4:
                    member.ViewAvailableBooks();
                    break;
                case 5:
                    member.ChangePassword();
                    break;
                case 6:
                    Console.WriteLine($"Member Details:\n{member}");
                    break;
                case 7:
                    ShowPenalty(member);
                    break;
                case 8:
                    Console.WriteLine("Logging out. Goodbye!");
                    Program.ShowUserOptions();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }

        private static void BorrowBookMenu(Member member )
        {
           
            member.ViewAvailableBooks();
            Console.Write("\nEnter the Book ID to borrow: ");
            string bookIdInput = Console.ReadLine();
            member.BorrowBook(bookIdInput);
            // Implement the logic for borrowing a book
        }

        private static void ReturnBookMenu(Member member)
        {
            
            member.LoanList();
            if (member.Loans.Count == 0)
            {
                Console.WriteLine("");

            }
            else
            {
                Console.Write("\nEnter the Book ID to return: ");
                string bookIdToReturn = Console.ReadLine();
                member.ReturnBook(bookIdToReturn);

            }



        }

        private static void ShowPenalty(Member member)
        {
            
            if (member.Loans.Count == 0)
            {
                Console.WriteLine("No penalty!");

            }
            else
            {
                member.CalculatePenaltyFee();
            }

        }
    }
}
