using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    class Program
    {
        public static Library library = new Library("City Library", "123 Main St");
       

        static void Main(string[] args)
        {
            library.LoadData();
            InitializeLibrary();

            Console.WriteLine("\n\n---------------------Welcome to the Library Management System!---------------------");

            while (true)
            {
                ShowUserOptions();
                Library.SaveData();
               
            }
            
           
        }

        public static void ShowUserOptions()
        {
            Console.WriteLine("\nChoose the user type:");
            Console.WriteLine("1. Librarian");
            Console.WriteLine("2. Member");
            Console.WriteLine("3. Exit");
            Console.WriteLine(new string('-', 83));

            try
            {
                string choice = Console.ReadLine();
                int numericChoice = int.Parse(choice);

                switch (numericChoice)
                {
                    case 1:
                        Librarian_UI.LibrarianLogin();
                        break;
                    case 2:
                        Member_UI.MemberLogin();
                        break;
                    case 3:
                        Library.SaveData();
                        Console.WriteLine("Exiting the program. Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter the numeric option.");
            }
            finally { 
                Console.WriteLine("Exception Handled");
            }
        }

        static void InitializeLibrary()
        {
           
            // Create some books
          
            Library.AddBook(new Book("B001", "The Art of Programming", "John Coder", 123456789, true));
            Library.AddBook(new Book("B002", "Design Patterns", "Gang of Four", 987654321, true));
            Library.AddBook(new Book("B003", "The Catcher in the Rye", "J.D. Salinger", 1235869,true));

            // Create a member
        
            Library.AddMember(new Member("M001", "pwd1", "Alice Mark", new DateTime(1990, 5, 5), "Main St.", new List<Loan>()));
            Library.AddMember(new Member("M002", "pwd2", "Ace Miller", new DateTime(1998, 5, 5), "Main St.", new List<Loan>()));

            // Create a librarian
            
            Library.AddLibrarian(new Librarian("L001", "lp12", "John Gates", new DateTime(1980, 1, 1), "Library St.", 20.0, 40));
        }


    }
}