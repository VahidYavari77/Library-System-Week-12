using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_system__Vahid_Yavari__HW_WEEK_12.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }   
        public int Rating { get; set; }       
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
