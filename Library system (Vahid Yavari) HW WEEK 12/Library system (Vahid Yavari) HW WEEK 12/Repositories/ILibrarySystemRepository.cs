using Library_system__Vahid_Yavari__HW_WEEK_12.Dto;
using Library_system__Vahid_Yavari__HW_WEEK_12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Repositories
{
    public interface ILibrarySystemRepository
    {
        public void AddUserInDb(User user);
        public void AddBookInDb(Book book);
        public void AddCategoryInDb(Category category);
        public List<User> GetUsersList();
        public void ActivationIsLoggedIn(User user);
        public void DeactivationIsLoggedIn(User user);
        public List<Book> GetBookList();
        public List<Category> GetCategories();
        public List<Book> GetBooksByCategoryIdFromCategoriesFromDB(int categoryID);
        public List<BookDisplayModel> GetBookAndCategory();
        public User GetCurrentUser();
        public Book GetBookByID(int BookId);
        public void AddBorrowBookInDb(BorrowedBook borrowedBook);
        public void ReturnBook(int bookId, int userId);
        public List<Book> GetBorrowedBooksByUser(int userId);
    }
}
