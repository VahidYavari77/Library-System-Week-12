using Library_system__Vahid_Yavari__HW_WEEK_12.Entities;
using Library_system__Vahid_Yavari__HW_WEEK_12.Enums;
using Library_system__Vahid_Yavari__HW_WEEK_12.Repositories;
using Library_system__Vahid_Yavari__HW_WEEK_12.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;
LibrarySystemService _lsService = new LibrarySystemService();
bool temp = false;
_lsService.DeActiveCurrentUser();
while (!temp)
{
    MainMenu();
    
}
void MainMenu()
{
    Console.Clear();
    Console.WriteLine("1.Login");
    Console.WriteLine("2.Register");
    Console.WriteLine("3.Exit");
    int choice = int.Parse(Console.ReadLine());
    switch (choice)
    {

        case 1: LoginUser(); break;
        case 2: RegisterUser(); break;
        case 3: temp = true; break;

    }
}
void LoginUser()
{
    try
    {
        Console.Clear();
        Console.Write("UserName : ");
        string username = Console.ReadLine()!.ToLower();
        Console.Write("Password : ");
        string password = Console.ReadLine()!;

        bool IsAuthentication = _lsService.Authentication(password, username);
        if (IsAuthentication)
        {
            int role = _lsService.GetUserRole();
            if (role == 2)
            {
                AdminMenu();
                _lsService.DeActiveCurrentUser();
            }
            if (role == 1)
            {
                MemberMenu();
                _lsService.DeActiveCurrentUser();
            }

        }
        else
        {
            throw new Exception("UserName Or Password Or  Is Invalid Or Activation => DeActive");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();

    }

}

void RegisterUser()
{
    Console.Clear();
    User user = new User();
    Console.Write("Insert First Name : ");
    user.FirstName = Console.ReadLine()!;
    Console.Write("Insert Last Name : ");
    user.LastName = Console.ReadLine()!;
    Console.Write("Insert User Name : ");
    user.UserName = Console.ReadLine()!;
    Console.Write("Insert Password : ");
    user.SetPassword(Console.ReadLine()!);
    Console.WriteLine("1.Member");
    Console.WriteLine("2.Admin");
    int role = int.Parse(Console.ReadLine()!);
    user.Role = (RoleEnum)role;
    if (role == 1)
    {
        user.IsActive = true;   
    }
    if (role==2)
    {
        user.IsActive = false;
    }
    Console.Write("Insert Mobile : ");
    user.Mobile = Console.ReadLine()!;
    _lsService.AddUser(user);
    Console.Clear();
    Console.WriteLine($"{user.FirstName} {user.LastName} was successfully registered in the user list.");
    Console.ReadKey();
}

void AdminMenu()
{
    Console.Clear();
    Console.WriteLine("---------- Admin Menu ----------");
    Console.WriteLine("");
    Console.WriteLine("1.Add new category");
    Console.WriteLine("2.Add a new book to a category");
    Console.WriteLine("3.View all books and categories");
    Console.WriteLine("4.Exit");
    int select = Convert.ToInt32(Console.ReadLine()!);
    switch (select)
    {
        case 1: NewCategory(); break;
        case 2: AddNewBookInCategory(); break;
        case 3: ViewBooksAndCategories(); break;
        case 4: return;
        default: AdminMenu(); break;
    }
    AdminMenu();

}
void MemberMenu()
{
    Console.Clear();
    Console.WriteLine("---------- Member Menu ----------");
    Console.WriteLine("");
    Console.WriteLine("1.View list of categories and books");
    Console.WriteLine("2.Choosing and borrowing books");
    Console.WriteLine("3.View your list of borrowed books");
    Console.WriteLine("4.Return borrowed Books");
    Console.WriteLine("5.Exit");
    int select = Convert.ToInt32(Console.ReadLine()!);
    switch (select)
    {
        case 1: ViewListOfCategoriesAndBooks(); break;
        case 2: ChoosingAndBorrowingBooks(); break;
        case 3: ViewListOfborrowedBooks(); break;
        case 4: ReturnborrowedBooks();break ;
        case 5: return;
        default: MemberMenu(); break;
    }
    MemberMenu();



}
void NewCategory()
{
  
    try
    {
        Console.Clear();
        Console.WriteLine("---------- Create a new category ----------");
        Console.Write("Insert Category Name : ");
        string categoryName = Console.ReadLine()!;
        Category category = new Category();
        category.CategoryName = categoryName;
        _lsService.AddCategory(category);
        Console.Clear();
        Console.WriteLine($"{categoryName} category added successfully.");
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
    }

}
void AddNewBookInCategory()
{
    Console.Clear();
    Console.WriteLine("---------- Add a new book to a category ----------");
    Book book = new Book();
    Console.Write("Insert Book Name :");
    book.Title = Console.ReadLine()!;
    Console.Write("Insert Author Name");
    book.Author = Console.ReadLine()!;

    foreach (var category in _lsService.GetCategoryList())
    {
        Console.WriteLine($"{category.Id} => {category.CategoryName}");
    }
    Console.Write("Insert Category :");
    book.CategoryId = int.Parse(Console.ReadLine()!); ;

    _lsService.AddBook(book);
    Console.Clear();
    Console.WriteLine($"{book.Title} successfully added to the library");
    Console.ReadKey();

}
void ViewBooksAndCategories()
{
    Console.Clear();
    foreach (var book in _lsService.GetBookAndCategory())
    {
        Console.WriteLine($"Id:{book.Id}=>    Title : {book.Title}---Author:{book.Author}---CategoryName : {book.CategoryName}---IsBorrowed:{book.IsBorrowed}");
    }
    Console.ReadKey();
}
void ViewListOfCategoriesAndBooks()
{
    Console.Clear();
    ViewListOfCategories();
    Console.WriteLine("Select Category ID : ");
    int categoryID = int.Parse(Console.ReadLine()!);
    ViewListOfBooksByID(categoryID);
    Console.Clear();
    Console.WriteLine("1.Continue");
    Console.WriteLine("2.Exit");
    int select = int.Parse(Console.ReadLine()!);
    if (select==2)
    {
        return;
    }
    ViewListOfCategoriesAndBooks();

}
void ViewListOfCategories()
{
    foreach (var category in _lsService.GetCategoryList())
    {
        Console.WriteLine($"{category.Id} => {category.CategoryName}");
    }

}
void ViewListOfBooksByID(int categoryID)
{
    Console.Clear();
    foreach (var category in _lsService.GetCategoryList())
    {
        if (category.Id== categoryID)
        {
            Console.WriteLine($"----------- {category.Id} => {category.CategoryName} ----------");
        }
    }

    foreach (var book in _lsService.GetBooksByCategoryIdFromCategories(categoryID))
    {
        Console.WriteLine($"Id:{book.Id}=>    Title : {book.Title}---Author:{book.Author}---IsBorrowed:{book.IsBorrowed}");
    }

    Console.ReadKey();

}
void ChoosingAndBorrowingBooks()
{

    try
    {
        Console.Clear();
        ViewListOfCategories();
        Console.WriteLine("Select Category ID : ");
        int categoryID = int.Parse(Console.ReadLine()!);
        ViewListOfBooksByID(categoryID);
        Console.WriteLine("Choose a book to borrow by id : ");
        int choice = Convert.ToInt32(Console.ReadLine()!);
        CreateBorrowedBook(_lsService.GetCurrentUser(), _lsService.GetBookByIDInRepo(choice));
        Console.Clear();
        Console.WriteLine("1.Continue");
        Console.WriteLine("2.Exit");
        int select = int.Parse(Console.ReadLine()!);
        if (select == 2)
        {
            return;
        }
        ChoosingAndBorrowingBooks();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
    }
}
void CreateBorrowedBook(User user,Book book)
{
    if (book.IsBorrowed)
    {
        throw new Exception("The book is not available");
        
    }
    BorrowedBook borrowedBook = new BorrowedBook();
    borrowedBook.BorrowDate = DateTime.Now;
    borrowedBook.User = user;
    borrowedBook.UserId = user.Id;
    borrowedBook.Book = book;
    borrowedBook.BookId = book.Id;
    borrowedBook.ReturnDate = null;
    user.ListOfBorrowedBooks.Add(borrowedBook);
    book.BorrowedBooks.Add(borrowedBook);
    book.IsBorrowed = true;
    _lsService.AddBorrowBookInlist(borrowedBook);
    Console.WriteLine($"{book.Title} successfully added to the list");
    Console.ReadKey();
}
void ReturnborrowedBooks()
{
    Console.Clear();
    ViewListOfborrowedBooks();
    Console.WriteLine("Insert BookID For Return Book : ");
    int bookID = int.Parse(Console.ReadLine()!);
    _lsService.ReturnBook(bookID, _lsService.GetCurrentUser().Id);
    Console.Clear();
    Console.WriteLine("1.Continue");
    Console.WriteLine("2.Exit");
    int select = int.Parse(Console.ReadLine()!);
    if (select == 2)
    {
        return;
    }
    ReturnborrowedBooks();

}
void ViewListOfborrowedBooks()
{
    Console.Clear();
    foreach (var book in _lsService.GetBorrowBookCurrentUser(_lsService.GetCurrentUser().Id))
    {
        Console.WriteLine($"Id:{book.Id}=>    Title : {book.Title}---Author:{book.Author}---IsBorrowed:{book.IsBorrowed}");
    }
    Console.ReadKey();
}










Console.Clear();
Console.WriteLine("Goodbye!");
