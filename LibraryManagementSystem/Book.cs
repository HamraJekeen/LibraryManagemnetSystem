using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book
    {
        public string BookID { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool Availability { get; set; }

        public Book(string bookID, string bookTitle, string bookAuthor, int bookISBN, bool availability)
        {
            BookID = bookID;
            Title = bookTitle;
            Author = bookAuthor;
            ISBN = bookISBN;
            Availability = availability;
        }

        public bool CheckBookAvailability()
        {
            return Availability;
        }

        public string GetDetails()
        {
            return $"BookID: {BookID}, ISBN: {ISBN}, Title: {Title}, Author: {Author}, Availability: {(Availability ? "Available" : "Not Available")}";
        }
    }
}
