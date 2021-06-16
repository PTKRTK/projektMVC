using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}