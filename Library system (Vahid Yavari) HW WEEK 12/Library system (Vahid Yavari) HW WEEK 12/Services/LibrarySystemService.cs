using Library_system__Vahid_Yavari__HW_WEEK_12.Dto;
using Library_system__Vahid_Yavari__HW_WEEK_12.Entities;
using Library_system__Vahid_Yavari__HW_WEEK_12.Enums;
using Library_system__Vahid_Yavari__HW_WEEK_12.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Services
{
    public class LibrarySystemService : ILibrarySystemService
    {
        ILibrarySystemRepository _repo=new EFCoreRepo();
        public void AddBook(Book book)
        {
            _repo.AddBookInDb(book);
        }
        public List<BookDisplayModel> GetBookAndCategory()
        {
            return _repo.GetBookAndCategory();     
        }


        public void AddCategory(Category category)
        {
            _repo.AddCategoryInDb(category);
        }

        public void AddUser(User user)
        {
            _repo.AddUserInDb(user);
        }
        
        public int GetUserRole()
        {
            foreach (var user in _repo.GetUsersList())
            {
                if (user.IsLoggedIn==true)
                {
                    return (int)user.Role;
                }
            }
            throw new Exception("Not Found Role");
        }
        public bool Authentication(string password, string username)
        {
            var foundUser = _repo.GetUsersList().FirstOrDefault(user =>
         user.UserName.ToLower() == username && Equals(password,user.GetPassword()) && user.IsActive == true);

            if (foundUser != null)
            {
                _repo.ActivationIsLoggedIn(foundUser);
                return true;

            }
            else
            {
                return false;
            }
        }

        public void DeActiveCurrentUser()
        {
            foreach (var user in _repo.GetUsersList())
            {
                if (user.IsLoggedIn==true)
                {
                    _repo.DeactivationIsLoggedIn(user);
                    return;
                }
            }
        }

        public List<Book> GetBooks()
        {
            return _repo.GetBookList();
        }

        public List<Category> GetCategoryList()
        {
            return _repo.GetCategories();
        }

        public List<Book> GetBooksByCategoryIdFromCategories(int categoryID)
        {
          return  _repo.GetBooksByCategoryIdFromCategoriesFromDB(categoryID);
        }

        public User GetCurrentUser()
        {
            return _repo.GetCurrentUser();
        }

        public Book GetBookByIDInRepo(int BookId)
        {
            return _repo.GetBookByID(BookId);
        }

        public void AddBorrowBookInlist(BorrowedBook borrowedBook)
        {
            _repo.AddBorrowBookInDb(borrowedBook);
        }

        public void ReturnBook(int bookID, int userID)
        {
            _repo.ReturnBook(bookID, userID);
        }

        public List<Book> GetBorrowBookCurrentUser(int userID)
        {
            return _repo.GetBorrowedBooksByUser(userID);

        }
    }
}
