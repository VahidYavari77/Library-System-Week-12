using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Entities
{
   public class Book
    {
  
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public bool IsBorrowed { get; set; } = false;
        public List<BorrowedBook> BorrowedBooks { get; set; } = [];
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
