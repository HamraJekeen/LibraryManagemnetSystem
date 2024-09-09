using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Loan
    {
        public string LoanID { get; set; }
        
        public Book BorrowedBook { get; set; }
        public DateTime DueDate { get; set; }

        public Loan(string loanID,String bookID, string bookTitle, string bookAuthor, int bookISBN)
        {
            LoanID = loanID;    
           
            BorrowedBook = new Book(bookID, bookTitle, bookAuthor, bookISBN, false); // Creating a new Book instance for loan
            SetDueDate(this); // Set due date during loan creation
        }

        public static void SetDueDate(Loan loan)
        {
            // a book can be borrowed for 14 days only
            loan.DueDate = DateTime.Now.AddDays(14);
        }
      

    }
}
