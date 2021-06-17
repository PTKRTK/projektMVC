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
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Role
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
            {
                return View();
            }
        [Authorize(Roles = "Admin")]
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
                im.AddUserToRoleByUserEmail("admin@mail.pl", "Admin");
                im.AddUserToRoleByUserEmail("patryk@mail.pl", "Employer");
             im.AddUserToRoleByUserEmail("piotr@mail.pl", "User");
            return "RolesPrzydzielone";
            }
        [Authorize(Roles = "Admin")]
        public ActionResult AddToRole()
        {
            return View();
        }

    
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddToRole(string email, string RoleName)
        {
            var users = db.Users.ToList();

            foreach (ApplicationUser u in users)    {
                if (u.UserName.Equals(email))
                {
                    if (RoleName.Equals("Admin") || RoleName.Equals("User") || RoleName.Equals("Employer"))
                    {
                        IdentityManager im = new IdentityManager();
                        im.AddUserToRoleByUserEmail(email, RoleName);

                        return Redirect("Index");
                    }
                }
            }
  

            return Redirect("AddToRole");

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUserRole()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteUserRole(string email)
        {
      
                        IdentityManager im = new IdentityManager();
                       // im.ClearUserRolesByUserMail(email);
           //         im.ClearUserRoles("df8cefe2 - ca06 - 4791 - bd29 - 003f9bc0db10");
                        return Redirect("Index");
             
    

        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddAnnouncement()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddAnnouncement(string announcementText)
        {
            Announcement announcement = new Announcement();
            announcement.AnnouncementText = announcementText;
            announcement.PublicationDate = DateTime.Now;
            db.Announcements.Add(announcement);
            db.SaveChanges();

            return Redirect("AddAnnouncement");

        }





    }
}