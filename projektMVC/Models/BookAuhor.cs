using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class BookAuthor
    {
        public int BookAuthorID { get; set; }

        
        public int BookID { get; set; }
        public virtual Book Book { get; set; }

        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }

    }
}