using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Member : Person
    {
        public string MembershipID { get; set; }
        public string Password { get; set; }
        public double PenaltyFee { get; set; }
        public List<Loan> Loans { get; set; }


        public Member(string memid, string password, string name, DateTime dateofBirth, string address, List<Loan> borrowed)
            : base(name, dateofBirth, address)
        {
            MembershipID = memid;
            Password = password;
            Loans = borrowed;

        }
       
        public void ViewAvailableBooks()
        {
            Console.WriteLine("\nAvailable Books:");
            Console.WriteLine("{0,-11} {1,-28} {2,-20} {3,-15}", " Book ID | ", "Title  ", "Author  ", "Availability");
            Console.WriteLine(new string('-', 75));

            foreach (Book book in Library.Books)
            {
                if (book.Availability) {

                   Console.WriteLine("{0,-11} {1,-28} {2,-20} {3,-15}",
                   book.BookID,
                   book.Title,
                   book.Author,
                   book.Availability ? "Available" : "Not Available");

                }
                   
            }

            Console.WriteLine();

        }

        public void BorrowBook(string bookId)
        {
            // Check if the member has already borrowed the book
            if (Loans.Any(loan => loan.BorrowedBook.BookID == bookId))
            {
                Console.WriteLine($"Book {bookId} is already borrowed by Member {Name}.");
                return;
            }
            Book book = Library.Books.Find(b => b.BookID == bookId);

            if (book != null && book.Availability)
            {

                Loan newLoan = new Loan($"L{Loans.Count+1}",book.BookID, book.Title, book.Author, book.ISBN);
                Loan.SetDueDate(newLoan);
                Loans.Add(newLoan);
               
                book.Availability = false;
               
              
                Console.WriteLine($"\n| Borrowed Book Name: {book.Title} | Borrowed Book Id: {book.BookID}");
                Console.WriteLine("Book Borrowed Successfully!");
            }
            else if (book == null)
            {
                Console.WriteLine($"Invalid book ID: {bookId}.\nBook not found in the library.");

            }

            else
            {
                Console.WriteLine($"Book {bookId} is not available for borrowing.");
            }

        }
        public void LoanList()
        {
            if (Loans.Count == 0)
            {
                Console.WriteLine("You have not borrowed any books.");

            }

            else
            {
                Console.WriteLine($"\nBorrowed Books by Member {Name} - {MembershipID}:");
                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-15} {4,15}","loan Id", "Book ID", "Title", "Author", "Due Date");
                Console.WriteLine(new string('-', 79));

                foreach (Loan loan in Loans)
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-15} {4,15}",
                        loan.LoanID,
                        loan.BorrowedBook.BookID,
                        loan.BorrowedBook.Title,
                        loan.BorrowedBook.Author,
                        loan.DueDate.ToShortDateString());
                }
               
              
            }

            Console.WriteLine();
        }

        public void CalculatePenaltyFee()
        {
            const double PenaltyRatePerDay = 50.0; // Penalty rate per day 


            foreach (Loan loan in Loans)
            {
                if (loan.DueDate < DateTime.Now && Loans.Count != 0)
                {
                    // Calculate the number of days overdue
                    int daysOverdue = (int)(DateTime.Now - loan.DueDate).TotalDays;

                    // Calculate penalty fee
                    double penalty = daysOverdue * PenaltyRatePerDay;

                    // Update member's penalty fee
                    PenaltyFee += penalty;

                    Console.WriteLine($"Book {loan.BorrowedBook.BookID} is {daysOverdue} days overdue. Penalty fee added: {penalty:c}");
                }
                else
                {
                    Console.WriteLine($"Book {loan.BorrowedBook.BookID} is not overdue.");
                }


            }
        }
        public void ReturnBook(string bookId)
        {
            if (Loans.Count == 0)
            {
                Console.WriteLine("You have not borrowed any books.");
                return;
            }

            Loan loanToReturn = Loans.Find(loan => loan.BorrowedBook.BookID == bookId);

            if (loanToReturn != null)
            {
                Book book = loanToReturn.BorrowedBook; // Get the book from the loan

                // Update book availability to true
                book.Availability = true;

                // Remove the loan from the member's Loans list
                Loans.Remove(loanToReturn);

                // Remove the loan from the library's loans list
                Library.loans.Remove(loanToReturn);
                

                // Update the book's availability in the library's Books list
                Book libraryBook = Library.Books.Find(b => b.BookID == bookId);
                if (libraryBook != null)
                {
                    libraryBook.Availability = true;
                   
                }

                Console.WriteLine($"Member {Name} returned book {bookId} successfully!");
               
            }
            else
            {
                Console.WriteLine($"Book {bookId} is not currently on loan by Member {Name}.");
            }
        }

        public void ChangePassword()
        {
            Console.Write("\nEnter current password: ");
            string currentPassword = Console.ReadLine();

            if (currentPassword == Password)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                Password = newPassword;
                Console.WriteLine($"{Name} password changed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid current password. Password change failed.");
            }
        }


        public override string ToString()
        {
            return $"|Member Name:{Name} | MembershipID: {MembershipID} | Address: {Address}| PenaltyFee: {PenaltyFee:c}";
        }
    }
}
