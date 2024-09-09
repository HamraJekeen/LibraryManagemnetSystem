using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Librarian_UI
    {
        public static void LibrarianLogin()
        {
            Console.WriteLine(new string('-', 83));
            Console.WriteLine("Librarian Login");
            Console.Write("Enter Librarian ID: ");
            string librarianId = Console.ReadLine();
            Console.Write("Enter Librarian Password: ");
            string librarianPassword = Console.ReadLine();

            // Validate librarian login
            if (ValidateLibrarianLogin(librarianId, librarianPassword))
            {
                Librarian loggedInLibrarian = Library.Librarians.Find(l => l.LibrarianID == librarianId);
                if (loggedInLibrarian != null)
                {
                    
                    Console.WriteLine($"\nWelcome, {loggedInLibrarian.Name}!");
                    LibrarianMenu(loggedInLibrarian);
                    Console.WriteLine(new string('-', 83));
                }
                else
                {
                    Console.WriteLine("Librarian not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Librarian ID or Password.");
                Program.ShowUserOptions();
            }
        }

        public static bool ValidateLibrarianLogin(string libID, string libPwd)
        {
            List<Librarian> librarians = Library.Librarians;
            Librarian loggedInLibrarian = librarians.Find(librarian => librarian.LibrarianID == libID);
            if (loggedInLibrarian != null)
            {
                return loggedInLibrarian.LibrarianPwd == libPwd;
            }

            return false;
        }

        public static void LibrarianMenu(Librarian librarian)
        {
            while (true)
            {
                Console.WriteLine(new string('-', 83));
                Console.WriteLine($"\nLibrarian Menu - {librarian.Name}");
                Console.WriteLine(new string('-', 83));
                Console.WriteLine("\nPlease enter the correct numeric option");
                Console.WriteLine("1. Add Member");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Remove Book");
                Console.WriteLine("4. Remove Member");
                Console.WriteLine("5. View Member Details");
                Console.WriteLine("6. View Available Books");
                Console.WriteLine("7. View all books");
                Console.WriteLine("8. Change Librarian Password");
                Console.WriteLine("9. View Librarian Details");
                Console.WriteLine("10. Logout");

                Console.Write("Enter your choice: ");
                try
                {
                    string choice = Console.ReadLine();
                    int numChoice = int.Parse(choice);

                    // Call the method to process the menu choice
                    ProcessLibrarianMenuChoice(numChoice, librarian);
                    Console.WriteLine(new string('-', 83));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a numeric option.");
                    Console.WriteLine(new string('-', 83));
                }
            }
        }
        private static void ProcessLibrarianMenuChoice(int choice,Librarian librarian)
        {
            switch (choice)
            {
                case 1:
                    ViewMembers();
                    MemberSignUp();
                    break;
                case 2:
                    librarian.ViewBooks();
                    AddBook();
                    break;
                case 3:
                    librarian.ViewBooks();
                    RemoveBook();
                    break;
                case 4:
                    ViewMembers();
                    RemoveMember();
                    break;
                case 5:
                    ViewMembers();
                    break;
                case 6:
                    Library.DisplayAvailableBooks();
                    break;
                case 7:
                    librarian.ViewBooks();
                    break;

                
                case 8:
                    librarian.ChangePassword();
                    break;
                case 9:
                    Console.WriteLine($"Librarian Details:\n{librarian}");
                    break;

                case 10:
                    Program.ShowUserOptions();
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }

        private static void MemberSignUp()
        {
            Console.Write("Enter Member ID: ");
            string memberId = Console.ReadLine();
            // Check if a book with the same BookID already exists
            if (Library.Members.Any(member => member.MembershipID == memberId))
            {
                Console.WriteLine($"A Member with ID '{memberId}' already exists in the library. Cannot add duplicate members.");
                return;
            }
            Console.Write("Enter Member Password: ");
            string memberPassword = Console.ReadLine();
            Console.Write("Enter Member Name: ");
            string memberName = Console.ReadLine();
            Console.Write("Enter Member Birthdate (yyyy-MM-dd): ");
            DateTime memberBirthdate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out memberBirthdate))
            {
                Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
            }
            Console.Write("Enter Member Address: ");
            string memberAddress = Console.ReadLine();

            Member newMember = new Member(memberId, memberPassword, memberName, memberBirthdate, memberAddress, new List<Loan>());
            Library.AddMember(newMember);
            Console.WriteLine($"Member '{newMember.Name}' added to the library.");
        }

        private static void RemoveMember()
        {
            Console.Write("Enter Member ID to remove: ");
            string memberIdToRemove = Console.ReadLine();
            Member memberToRemove = Library.Members.Find(m => m.MembershipID == memberIdToRemove);
            if (memberToRemove != null)
            {
                Library.RemoveMember(memberToRemove);
            }
            else
            {
                Console.WriteLine($"Member with ID '{memberIdToRemove}' not found in the library.");
            }
        }

        public static void AddBook()
        {
            Console.Write("Enter Book ID: ");
            string bookId = Console.ReadLine();

            // Check if a book with the same BookID already exists
            if (Library.Books.Any(book => book.BookID == bookId))
            {
                Console.WriteLine($"A book with ID '{bookId}' already exists in the library. Cannot add duplicate books.");
                return;
            }

            Console.Write("Enter Book Title: ");
            string bookTitle = Console.ReadLine();
            Console.Write("Enter Book Author: ");
            string bookAuthor = Console.ReadLine();
            Console.Write("Enter ISBN: ");
            string isbnInput = Console.ReadLine();

            if (int.TryParse(isbnInput, out int bookISBN))
            {
                Book newBook = new Book(bookId, bookTitle, bookAuthor, bookISBN, true);
                Library.AddBook(newBook);
                Console.WriteLine($"Book '{newBook.Title}' added to the library.");
            }
            else
            {
                Console.WriteLine("Invalid ISBN. Please enter a valid number.");
            }
        }

        private static void RemoveBook()
        {
            Console.Write("Enter Book ID to remove: ");
            string bookIdToRemove = Console.ReadLine();
            Book bookToRemove = Library.Books.Find(b => b.BookID == bookIdToRemove);
            if (bookToRemove != null)
            {
                Library.RemoveBook(bookToRemove);
            }
            else
            {
                Console.WriteLine($"Book with ID '{bookIdToRemove}' not found in the library.");
            }
        }
        public static void ViewMembers()
        {

            Console.WriteLine("\nAll Member Details:");
            Console.WriteLine("\n{0,-11} {1,-28} {2,-20} {3,-15}", " Member ID | ", "Member Name  |", " Date of Birth | ", "Member Address |");
            Console.WriteLine(new string('-', 75));

            foreach (Member member in Library.Members)
            {
                Console.WriteLine("{0,-11} {1,-28} {2,-20} {3,-15}",
                    member.MembershipID,
                    member.Name,
                    member.DOB.ToShortDateString(),
                    member.Address);


            }
            Console.WriteLine();


        }
    }
}
