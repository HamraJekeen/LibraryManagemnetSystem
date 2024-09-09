using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Librarian : Person
    {
        public string LibrarianID { get; set; }
        public string LibrarianPwd { get; set; }
        public double SalaryRate { get; set; }
        public int WorkHours { get; set; }

        public Librarian(string librarianid, string libPassword, string psname, DateTime psdob, string psadrs, double salaryAmount, int workhours)
            : base(psname, psdob, psadrs)
        {
            LibrarianID = librarianid;
            LibrarianPwd = libPassword;
            SalaryRate = salaryAmount;
            WorkHours = workhours;
        }

        public void CalculateSalary()
        {
            //double salary = SalaryRate * WorkHours;
            //Console.WriteLine($"Librarian {LibrarianID}'s salary is: {salary:C}");
        }
        public void ViewBooks()
        {
            Console.WriteLine("View all Books:");
            Console.WriteLine("{0,-11} {1,-28} {2,-20} {3,-15}", " Book ID | ", "Title  ", "Author  ", "Availability");
            Console.WriteLine(new string('-', 75));

            foreach (Book book in Library.Books)
            {
                Console.WriteLine("{0,-11} {1,-28} {2,-20} {3,-15}",
                    book.BookID,
                    book.Title,
                    book.Author,
                    book.Availability ? "Available" : "Not Available");
            }

            Console.WriteLine();

        }
        public void ChangePassword()
        {
            Console.Write("Enter current password: ");
            string currentPassword = Console.ReadLine();

            if (currentPassword == LibrarianPwd)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                LibrarianPwd = newPassword;
                Console.WriteLine("Librarian password changed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid current password. Password change failed.");
            }
        }




        public override string ToString()
        {
            return $", librarian name : {Name} | LibrarianID: {LibrarianID} | ";
        }

    }
}
