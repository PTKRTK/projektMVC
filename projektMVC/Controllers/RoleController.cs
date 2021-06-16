using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projektMVC.Models;

namespace projektMVC.Controllers
{
    public class RoleController : Controller
    {
  
            // GET: Role
            public ActionResult Index()
            {
                return View();
            }

            public string Create()
            {
                IdentityManager im = new IdentityManager();
                im.CreateRole("Employer");
                im.CreateRole("User");
                im.CreateRole("Admin");  

                return "RolesCreated";
            }

            //to dodac przy tworzeniu uzytkownika
            public string AddToRole2()
            {
                IdentityManager im = new IdentityManager();
                im.AddUserToRoleByUserEmail("admin@mail.pl", "Employer");
                im.AddUserToRoleByUserEmail("patryk@mail.pl", "User");
            im.AddUserToRoleByUserEmail("piotr@mail.pl", "User");
            return "RolesPrzydzielone";
            }

        public string AddAdmin()
        {
            IdentityManager im = new IdentityManager();
            im.AddUserToRoleByUserEmail("admin@mail.pl", "Employer");
            im.AddUserToRoleByUserEmail("patryk@mail.pl", "User");
            im.AddUserToRoleByUserEmail("piotr@mail.pl", "User");
            return "RolesPrzydzielone";
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddAdmin([Bind(Include = "email")] String email)
        {
            IdentityManager im = new IdentityManager();
            im.AddUserToRoleByUserEmail("admin@mail.pl", "Employer");
            im.AddUserToRoleByUserEmail("patryk@mail.pl", "User");
            im.AddUserToRoleByUserEmail("piotr@mail.pl", "User");
            return "RolesPrzydzielone";
        }






    }
}