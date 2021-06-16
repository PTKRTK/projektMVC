using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
	public class Book
	{
		public int BookID { get; set; }
        public string BookTitle { get; set; }

        public int PublicationYear { get; set; }

        public int PublishingHouseID { get; set; }
        public int BookCategoryID { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
        public virtual BookCategory BookCategory { get; set; }


        public virtual ICollection<BookCopy> BookCopies { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

    }
}