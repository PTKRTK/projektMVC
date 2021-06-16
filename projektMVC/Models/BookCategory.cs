using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class BookCategory
    {
        public int BookCategoryID { get; set; }
        public string CategoryTitle { get; set; }



        public virtual ICollection<Book> Books { get; set; }

    }
}