using LibraryManagementSystem;
namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange-Go get your Variables, classes, everything
            string BookId = "B001";
            string Title = "The Art of Programming";
            string Author = "John Coder";
            int ISBN = 123456789;
            bool Availabity = true;

            //Act-Execute this function
            Book Book1 = new Book(BookId, Title, Author, ISBN, Availabity);

            //Asserta- what ever is returned as what u expected
            Assert.NotNull(Book1);
            Assert.Equal(BookId, Book1.BookID);
            Assert.Equal(Title, Book1.Title);
            Assert.Equal(Author, Book1.Author);
            Assert.Equal(ISBN, Book1.ISBN);
            Assert.True(Availabity);


        }
        [Fact]
        public void MemberConstructor_Initialization_Successful()
        {
            // Arrange
            string membershipID = "M001";
            string password = "password123";
            string name = "Test Member";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            string address = "123 Main St";
            List<Loan> borrowedLoans = new List<Loan>();

            // Act
            Member member = new Member(membershipID, password, name, dateOfBirth, address, borrowedLoans);

            // Assert
            Assert.NotNull(member);
            Assert.Equal(membershipID, member.MembershipID);
            Assert.Equal(password, member.Password);
            Assert.Equal(name, member.Name);
            Assert.Equal(dateOfBirth, member.DOB);
            Assert.Equal(address, member.Address);
            Assert.NotNull(member.Loans);
            Assert.Equal(borrowedLoans, member.Loans);
        }
        [Fact]
        public void LibrarianConstructor_Initialization_Successful()
        {
            // Arrange
            string librarianID = "L001";
            string librarianPassword = "password123";
            string name = "Test Librarian";
            DateTime dateOfBirth = new DateTime(1980, 1, 1);
            string address = "123 Library St";
            double salaryAmount = 20.0;
            int workHours = 40;

            // Act
            Librarian librarian = new Librarian(librarianID, librarianPassword, name, dateOfBirth, address, salaryAmount, workHours);

            // Assert
            Assert.NotNull(librarian);
            Assert.Equal(librarianID, librarian.LibrarianID);
            Assert.Equal(librarianPassword, librarian.LibrarianPwd);
            Assert.Equal(name, librarian.Name);
            Assert.Equal(dateOfBirth, librarian.DOB);
            Assert.Equal(address, librarian.Address);
            Assert.Equal(salaryAmount, librarian.SalaryRate);
            
        }
        [Fact]
        public void CheckBookAvailability_ReturnsCorrectAvailability()
        {
            // Arrange
            Book book = new Book("B001", "The Art of Programming", "Test Author", 123456789, true);

            // Act
            bool availability = book.CheckBookAvailability();

            // Assert
            Assert.True(availability);
        }
        [Fact]
        public void GetDetails_ReturnsCorrectDetails()
        {
            // Arrange
            Book book = new Book("B001", "The Art of Programming", "John Coder", 123456789, true);
            // Act
            string details = book.GetDetails();

            // Assert
            Assert.Contains("BookID: B001", details);
            Assert.Contains("ISBN: 123456789", details);
            Assert.Contains("Title: The Art of Programming", details);
            Assert.Contains("Author: John Coder", details);
            Assert.Contains("Availability: Available", details);
        }
        //tests for member class 
        //test for borrow method
        [Fact]
        public void BorrowBook_BookAvailable_BorrowSuccessful()
        {
            // Arrange
            Member member = new Member("M001", "password", "Alice", new DateTime(1990, 5, 5), "Main St.", new List<Loan>());
            Book book = new Book("B001", "The Art of Programming", "John Coder", 123456789, true);
            Library.Books.Add(book);

            // Act
            member.BorrowBook("B001");

            // Assert
            Assert.Single(member.Loans);
            Assert.False(book.Availability);
        }

        [Fact]
        public void BorrowBook_BookAlreadyBorrowed_BorrowFailed()
        {
            // Arrange
            Member member = new Member("M001", "password", "Alice", new DateTime(1990, 5, 5), "Main St.", new List<Loan>());
            Book book = new Book("B001", "The Art of Programming", "John Coder", 123456789, false);
            member.Loans.Add(new Loan("L001", "B001", "The Art of Programming", "John Coder", 123456789));

            // Act
            member.BorrowBook("B001");

            // Assert
            Assert.Single(member.Loans);
            Assert.False(book.Availability);
        }
        [Fact]
        public void ReturnBook_SuccessfulReturn()
        {
            // Arrange
            Member member = new Member("M001", "password", "Alice", new DateTime(1990, 5, 5), "Main St.", new List<Loan>());
            Book book = new Book("B001", "The Art of Programming", "John Coder", 123456789, false);
            Loan loan = new Loan("L001", "B001", "The Art of Programming", "John Coder", 123456789);
            member.Loans.Add(loan);
            Library.Books.Add(book);

            // Act
            member.ReturnBook("B001");

            // Assert
            Assert.Empty(member.Loans);
            Assert.True(book.Availability);
        }

        [Fact]
        public void ReturnBook_BookNotOnLoan()
        {
            // Arrange
            Member member = new Member("M001", "password", "Alice", new DateTime(1990, 5, 5), "Main St.", new List<Loan>());
            Book book = new Book("B001", "The Art of Programming", "John Coder", 123456789, true);
            Library.Books.Add(book);

            // Act
            member.ReturnBook("B001");

            // Assert
            Assert.Empty(member.Loans);
            Assert.True(book.Availability);
        }
        [Fact]
        public void AddBook_BookAddedSuccessfully()
        {
            // Arrange
            Library library = new Library("Central Library", "Main Street");
            Book book = new Book("B008", "The Art of Programming", "John Coder", 123456789, true);

            // Act
            Library.AddBook(book);

            // Assert
            Assert.Contains(book, Library.Books);
        }
        [Fact]
        public void RemoveBook_BookRemovedSuccessfully()
        {
            // Arrange
            Book bookToRemove = new Book("B001", "The Art of Programming", "John Coder", 123456789, true);
            Library.Books = new List<Book> { bookToRemove }; // Set up the library with a single book

            // Act
            Library.RemoveBook(bookToRemove);

            // Assert
            Assert.Empty(Library.Books); // Ensure that the book is removed from the library
        }
        [Fact]
        public void AddMember_MemberAddedSuccessfully()
        {
            // Arrange
            Member newMember = new Member("M001", "password", "Alice", DateTime.Now, "123 Main St.", new List<Loan>());
            Library.Members = new List<Member>(); // Set up the library with an empty list of members

            // Act
            Library.AddMember(newMember);

            // Assert
            Assert.Single(Library.Members); // Ensure that the member is added to the library
            Assert.Contains(newMember, Library.Members); // Ensure that the specific member is in the library
        }
        [Fact]
        public void RemoveMember_MemberRemovedSuccessfully()
        {
            // Arrange
            Member existingMember = new Member("M001", "password", "Alice", DateTime.Now, "123 Main St.", new List<Loan>());
            Library.Members = new List<Member> { existingMember }; // Set up the library with one existing member

            // Act
            Library.RemoveMember(existingMember);

            // Assert
            Assert.Empty(Library.Members); // Ensure that the list of members is now empty
            Assert.DoesNotContain(existingMember, Library.Members); // Ensure that the specific member is not in the library
        }
        [Fact]
        public void AddLibrarian_LibrarianAddedSuccessfully()
        {
            // Arrange
            Librarian newLibrarian = new Librarian("L001", "librarianPwd", "John Librarian", DateTime.Now, "456 Main St.", 20.0, 40);

            // Act
            Library.AddLibrarian(newLibrarian);

            // Assert
            Assert.Contains(newLibrarian, Library.Librarians); // Ensure that the librarian is in the library's list of librarians
        }

        [Fact]
        public void RemoveLibrarian_LibrarianRemovedSuccessfully()
        {
            // Arrange
            Librarian existingLibrarian = new Librarian("L001", "librarianPwd", "John Librarian", DateTime.Now, "456 Main St.", 20.0, 40);
            Library.Librarians = new List<Librarian> { existingLibrarian }; // Set up the library with one existing librarian

            // Act
            Library.RemoveLibrarian(existingLibrarian);

            // Assert
            Assert.Empty(Library.Librarians); // Ensure that the list of librarians is now empty
            Assert.DoesNotContain(existingLibrarian, Library.Librarians); // Ensure that the specific librarian is not in the library
        }
        [Fact]
        public void ValidateLibrarianLogin_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");
            Librarian newLibrarian = new Librarian("L001", "librarianPwd", "John Librarian", DateTime.Now, "456 Main St.", 20.0, 40);

            // Act
            Library.AddLibrarian(newLibrarian);
           

            // Act
            bool result = Librarian_UI.ValidateLibrarianLogin("L001", "librarianPwd");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateLibrarianLogin_InvalidLibrarianID_ReturnsFalse()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");

            // Act
            bool result = Librarian_UI.ValidateLibrarianLogin("InvalidLibrarianID", "password123");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateLibrarianLogin_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");
            Library.AddLibrarian(new Librarian("L001", "password123", "John Doe", DateTime.Now, "123 Main St", 20.0, 40));

            // Act
            bool result = Librarian_UI.ValidateLibrarianLogin("L001", "InvalidPassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateLibrarianLogin_LibrarianNotFound_ReturnsFalse()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");

            // Act
            bool result = Librarian_UI.ValidateLibrarianLogin("NonexistentLibrarianID", "password123");

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ValidateMemberLogin_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");
            Library.AddMember(new Member("M001", "password123", "Alice", DateTime.Now, "123 Main St", new System.Collections.Generic.List<Loan>()));

            // Act
            bool result = Member_UI.ValidateMemberLogin("M001", "password123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateMemberLogin_InvalidMemberID_ReturnsFalse()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");

            // Act
            bool result = Member_UI.ValidateMemberLogin("InvalidMemberID", "password123");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateMemberLogin_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");
            Library.AddMember(new Member("M001", "password123", "Alice", DateTime.Now, "123 Main St", new System.Collections.Generic.List<Loan>()));

            // Act
            bool result = Member_UI.ValidateMemberLogin("M001", "InvalidPassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateMemberLogin_MemberNotFound_ReturnsFalse()
        {
            // Arrange
            Library library = new Library("Test Library", "Test Location");

            // Act
            bool result = Member_UI.ValidateMemberLogin("NonexistentMemberID", "password123");

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void LoanConstructor_Initialization_Successful()
        {
            // Arrange
            string loanID = "L001";
            string bookID = "B001";
            string bookTitle = "Test Book";
            string bookAuthor = "Test Author";
            int bookISBN = 123456789;

            // Act
            Loan loan = new Loan(loanID, bookID, bookTitle, bookAuthor, bookISBN);

            // Assert
            Assert.NotNull(loan);
            Assert.Equal(loanID, loan.LoanID);
            Assert.NotNull(loan.BorrowedBook);
            Assert.Equal(bookID, loan.BorrowedBook.BookID);
            Assert.Equal(bookTitle, loan.BorrowedBook.Title);
            Assert.Equal(bookAuthor, loan.BorrowedBook.Author);
            Assert.Equal(bookISBN, loan.BorrowedBook.ISBN);
            Assert.False(loan.BorrowedBook.Availability);

        }
      



    }
}