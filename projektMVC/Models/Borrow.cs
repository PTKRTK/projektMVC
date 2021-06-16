using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class Borrow
    {
        public int BorrowID { get; set; }
        [DisplayFormat(DataFormatString= "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime BorrowDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }


        public int BookCopyID { get; set; }
        public string UserID { get; set; }
        public int PunishmentID { get; set; }


        public virtual BookCopy BookCopy { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Punishment Punishment { get; set; }


    }
}