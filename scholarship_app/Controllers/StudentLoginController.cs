using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using scholarship_app.Models;

namespace scholarship_app.Controllers
{
    public class StudentLoginController : Controller
    {
        // GET: StudentLogin
        public ActionResult Index(student Student)
        {

            if (ModelState.IsValid)
            {
                Entities db = new Entities();
                var user = (from userlist in db.students
                            where userlist.Name == Student.Name && userlist.Email == Student.Email
                            select new
                            {
                                userlist.Id,
                                userlist.Name
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    FormsAuthentication.SetAuthCookie(user.FirstOrDefault().Name, true);
                  
                    Session["UserName"] = user.FirstOrDefault().Name;
                    Session["UserID"] = user.FirstOrDefault().Id;
                    return RedirectToAction("Index","scholarship_details");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View();
        }
    }
}