using Library_system__Vahid_Yavari__HW_WEEK_12.DataAccess;
using Library_system__Vahid_Yavari__HW_WEEK_12.Dto;
using Library_system__Vahid_Yavari__HW_WEEK_12.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Repositories
{

    public class EFCoreRepo : ILibrarySystemRepository
    {
        AppDbContext _context = new AppDbContext();

        public void ActivationIsLoggedIn(User user)
        {

            user.IsLoggedIn = true;
            _context.SaveChanges();

        }

        public void AddBookInDb(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();


        }


        public void AddCategoryInDb(Category category)
        {
            _context.Categorys.Add(category);
            _context.SaveChanges();
        }

        public void AddUserInDb(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeactivationIsLoggedIn(User user)
        {
            user.IsLoggedIn = false;
            _context.SaveChanges();
        }

        public List<Book> GetBookList()
        {
            return _context.Books.ToList();
        }

        public List<Book> GetBooksByCategoryIdFromCategoriesFromDB(int categoryID)
        {
            var booksWithCategoryIdOne = _context.Categorys
                                             .Include(p => p.BooksList) 
                                             .SelectMany(c => c.BooksList) 
                                             .Where(book => book.CategoryId == categoryID) 
                                             .ToList();
            return booksWithCategoryIdOne;
        }

        public List<Category> GetCategories()
        {
            return _context.Categorys.ToList();
        }

        public List<User> GetUsersList()
        {
            return _context.Users.ToList();
        }

        public List<BookDisplayModel> GetBookAndCategory()
        {
            var booksWithCategoryInfo = _context.Books
                                        .Include(b => b.Category)
                                        .Select(book => new BookDisplayModel 
                                        {
                                            Id = book.Id,
                                            Title = book.Title,
                                            Author = book.Author,
                                            
                                            CategoryName = book.Category == null ? null : book.Category.CategoryName,
                                            IsBorrowed = book.IsBorrowed,
                                          
                                            BorrowedCount = book.BorrowedBooks == null ? 0 : book.BorrowedBooks.Count
                                        }).ToList();
            return booksWithCategoryInfo;
        }

        public User GetCurrentUser()
        {
            return _context.Users.Include(u => u.ListOfBorrowedBooks).FirstOrDefault(p => p.IsLoggedIn == true)
                ?? throw new Exception("Logged in user not found.");
        }

        public Book GetBookByID(int BookId)
        {
            return _context.Books.Include(u => u.BorrowedBooks).FirstOrDefault(p => p.Id== BookId)
                ?? throw new Exception("Logged in book not found."); ;
        }

        public void AddBorrowBookInDb(BorrowedBook borrowedBook)
        {
            _context.BorrowedBooks.Add(borrowedBook);
            _context.SaveChanges();
        }

        public void ReturnBook(int bookId,int userId)
        {
           
            var borrowedEntry = _context.BorrowedBooks
                .Include(bb => bb.Book)  
                .Include(bb => bb.User)  
                .FirstOrDefault(bb => bb.BookId == bookId && bb.ReturnDate == null&& userId == bb.UserId);

            if (borrowedEntry == null)
            {
                
                throw new Exception("This book is not currently borrowed by any user or has already been returned.");
            }

          
            borrowedEntry.ReturnDate = DateTime.UtcNow;

          
            var book = borrowedEntry.Book; 
            if (book != null)
            {
                book.IsBorrowed = false;
            }
            else
            {
                throw new Exception("Book information for the borrowed record was not found.");
            }
        }
        public List<Book> GetBorrowedBooksByUser(int userId)
        {
           
            var user = _context.Users
                .Include(u => u.ListOfBorrowedBooks) 
                    .ThenInclude(bb => bb.Book)    
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            var currentlyBorrowedBooks = user.ListOfBorrowedBooks
                .Where(bb => bb.ReturnDate == null) 
                .Select(bb => bb.Book)           
                .ToList();

            _context.SaveChanges();

            return currentlyBorrowedBooks; 
        }
    }
}
