using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class PublishingHouse
    {
        public int PublishingHouseID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}