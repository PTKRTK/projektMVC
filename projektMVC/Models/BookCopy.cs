using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class BookCopy
    {
        public int BookCopyID { get; set; }
        public int ISBN { get; set; }
        public int BookCopyReleaseYear { get; set; }
        public int BookID { get; set; }
        public string IsBorrowed { get; set; }
        public virtual Book Book { get; set; }

        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}