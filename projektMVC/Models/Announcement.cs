using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }
        public DateTime PublicationDate { get; set; }

        public string AnnouncementText { get; set; }


    }
}