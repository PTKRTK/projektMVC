using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektMVC.Models
{
    public class Punishment
    {
        public int PunishmentID { get; set; }
        public double Charge { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }

    }
}