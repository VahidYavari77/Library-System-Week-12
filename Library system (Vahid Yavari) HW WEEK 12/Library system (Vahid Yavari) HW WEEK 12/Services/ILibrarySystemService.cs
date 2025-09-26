using Library_system__Vahid_Yavari__HW_WEEK_12.Dto;
using Library_system__Vahid_Yavari__HW_WEEK_12.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Services
{
    public interface ILibrarySystemService
    {
        public void AddBook(Book book);
        public void AddUser(User user);
        public void AddCategory(Category category);
        public bool Authentication(string password ,string username);
        public void DeActiveCurrentUser();
        public List<Book> GetBooks();
        public List<Category> GetCategoryList();
        public List<BookDisplayModel> GetBookAndCategory();
        public List<Book> GetBooksByCategoryIdFromCategories(int categoryID);
        public User GetCurrentUser();
        public Book GetBookByIDInRepo(int BookId);
        public void AddBorrowBookInlist(BorrowedBook borrowedBook);
        public void ReturnBook(int bookID,int userID);
       
        public List<Book> GetBorrowBookCurrentUser(int userID);
    }
}
