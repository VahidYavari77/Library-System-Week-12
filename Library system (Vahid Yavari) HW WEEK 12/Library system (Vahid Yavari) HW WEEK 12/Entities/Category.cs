using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Entities
{
   public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Book> BooksList { get; set; }

    }
}
