using Library_system__Vahid_Yavari__HW_WEEK_12.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        private string Password { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public bool IsActive { get; set; } 
        public List<BorrowedBook> ListOfBorrowedBooks { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();


        public string GetPassword()
        {
            return Password;
        }
        public  void SetPassword(string pass)
        {
            Password = pass;
        }

    }
}
