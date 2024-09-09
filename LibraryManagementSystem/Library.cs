using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryManagementSystem
{
    public class Library
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public  static List<Book> Books=new List<Book>();
        public static List<Member> Members = new List<Member>();
        public static List<Librarian> Librarians = new List<Librarian>();
        public static List<Loan> loans = new List<Loan>();

        private const string MemberFileName = "MembersDetail.txt";
        private const string LibrarianFileName = "LibrariansDetail.txt";
        private const string BookFileName = "BookDetailss.txt";
        private const string LoanFileName = "Loandetail.txt";


        public Library(string name, string location)
        {
            Name = name;
            Location = location;

        }
        public static void AddBook(Book book)
        {
            if (!Books.Any(b => b.BookID == book.BookID))
            {
                Books.Add(book);
                SaveData();
            }
            else
            {
                //Console.WriteLine($"Book with ID '{book.BookID}' already exists in the library. Cannot add duplicates.");
            }
        }

        public static void RemoveBook(Book book)
        {
            Books.Remove(book);
            Console.WriteLine($"Book '{book.Title}' removed from the library.");
            SaveBooks();
            
        }

        public static void DisplayAvailableBooks()
        {
            Console.WriteLine("\nAvailable Books:");
            Console.WriteLine("\n{0,-11} {1,-28} {2,-20} {3,-15}", "Book ID", "Title", "Author", "Availability");
            Console.WriteLine(new string('-', 74));

            foreach (Book book in Books)
            {
                if (book.Availability)
                {
                    Console.WriteLine("{0,-11} {1,-28} {2,-20} {3,-15}",
                        book.BookID,
                        book.Title,
                        book.Author,
                        book.Availability ? "Available" : "Not Available");
                }
            }

            Console.WriteLine();
        }


        public static void AddMember(Member member)
        {
            if (!Members.Any(m => m.MembershipID == member.MembershipID))
            {
                Members.Add(member);
                //Console.WriteLine($"Member '{member.Name}' added to the library.");
                SaveData();
            }
            else
            {
                //Console.WriteLine("Cannot add duplicates.");

            }


        }
        public static void RemoveMember(Member member)
        {
            Members.Remove(member);
            Console.WriteLine($"Member '{member.Name}' removed from the library.");
            SaveData();

        }
        public static void AddLibrarian(Librarian librarian)
        {
            if (!Librarians.Any(l => l.LibrarianID == librarian.LibrarianID))
            {
                Librarians.Add(librarian);
                //Console.WriteLine($"Librarian '{librarian.Name}' added to the library.");
                SaveData();
            }
           
        }
        public static void RemoveLibrarian(Librarian librarian)
        {
            Librarians.Remove(librarian);
            Console.WriteLine($"Librarian '{librarian.Name}' removed from the library.");
            SaveData();

        }

        //Save Members

        public static void SaveMembers()
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), MemberFileName);
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (Member member in Members)
                {
                    writer.WriteLine($"{member.MembershipID},{member.Password},{member.Name},{member.DOB},{member.Address}");
                }
            }
        }

        //Load Members
        public List<Member> LoadMembers()
        {
            List<Member> loadedMembers = new List<Member>();
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), MemberFileName);
            if (File.Exists(filepath))
            {
                string[] lines = File.ReadAllLines(filepath);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');

                    if (data.Length == 5)
                    {
                        string memID = data[0];
                        string password = data[1];
                        string name = data[2];
                        DateTime dob = DateTime.Parse(data[3]);
                        string address = data[4];

                        Member member = new Member(memID, password, name, dob, address, new List<Loan>());
                        loadedMembers.Add(member);
                    }
                }
            }

            Members = loadedMembers; // Update the class-level list
            return loadedMembers;
        }

        public static void SaveBooks()
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), BookFileName);
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (Book book in Books)
                {
                    writer.WriteLine($"{book.BookID},{book.Title},{book.Author},{book.ISBN},{book.Availability}");
                }
            }
        }

        public List<Book> LoadBooks()
        {
            List<Book> loadedBooks = new List<Book>();
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), BookFileName);
            if (File.Exists(filepath))
            {
                string[] lines = File.ReadAllLines(filepath);

                foreach (string line in lines)
                {
                    string[] bookData = line.Split(',');

                    if (bookData.Length == 5)
                    {
                        string bookID = bookData[0];
                        string title = bookData[1];
                        string author = bookData[2];
                        int isbn = int.Parse(bookData[3]);
                        bool availability = bool.Parse(bookData[4]);

                        Book book = new Book(bookID, title, author, isbn, availability);
                        loadedBooks.Add(book);
                    }
                }
            }
            Books = loadedBooks; // Update the class-level list
            return loadedBooks;
        }
        public static void SaveLibrarians()
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), LibrarianFileName);
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (Librarian librarian in Librarians)
                {
                    writer.WriteLine($"{librarian.LibrarianID},{librarian.LibrarianPwd},{librarian.Name},{librarian.DOB},{librarian.Address},{librarian.SalaryRate},{librarian.WorkHours}");
                }
            }
        }

        public void LoadLibrarians()
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), LibrarianFileName);
            if (File.Exists(filepath))
            {
                string[] lines = File.ReadAllLines(filepath);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');

                    if (data.Length == 7)
                    {
                        string librarianId = data[0];
                        string librarianPwd = data[1];
                        string name = data[2];
                        DateTime dateOfBirth = DateTime.Parse(data[3]);
                        string address = data[4];
                        double salaryRate = double.Parse(data[5]);
                        int workHours = int.Parse(data[6]);

                        Librarian librarian = new Librarian(librarianId, librarianPwd, name, dateOfBirth, address, salaryRate, workHours);
                    }
                }
            }
        }


        public static void SaveData()
        {
            SaveBooks();
            SaveLibrarians();
            SaveMembers();

        }

        public void LoadData()
        {
            LoadBooks();
            LoadLibrarians();
            LoadMembers();
            SaveData();
        }

    }
    
}
