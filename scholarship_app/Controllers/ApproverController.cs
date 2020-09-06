using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using scholarship_app.Models;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace scholarship_app.Controllers
{
    public class ApproverController : Controller
    {
        // GET: Approver
        private Entities db = new Entities();
        public ActionResult Index()
        {
            var scholarship_details = db.scholarship_details.Select(x => x);
            return View(scholarship_details.ToList());
        }


        public ActionResult Approve(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var scholarship_details = db.scholarship_details.Where(x => x.student_id == id).FirstOrDefault();
                scholarship_details.status = "Approved";
                db.SaveChanges();
                var scholarship_details_ = db.scholarship_details.Select(x => x);

                var senderEmail = new MailAddress("YOUR_EMAIL@gmail.com", "E-SCHOLARSHIP APP");
                var receiverEmail = new MailAddress(scholarship_details.student.Email, scholarship_details.student.Name );
                var password = "GIVE YOUR EMAIL PASSWORD HERE";
                var subject = "test subject";
                var body = "Your application has been Approved";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }



                return View("Index",scholarship_details_.ToList());
            }
        }


        public ActionResult Reject(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var scholarship_details = db.scholarship_details.Where(x => x.student_id == id).FirstOrDefault();
                scholarship_details.status = "Rejected";
                db.SaveChanges();
                var scholarship_details_ = db.scholarship_details.Select(x => x);

                var senderEmail = new MailAddress("YOUR_EMAIL@gmail.com", "E-SCHOLARSHIP APP");
                var receiverEmail = new MailAddress(scholarship_details.student.Email, scholarship_details.student.Name);
                var password = "GIVE YOUR EMAIL PASSWORD HERE";
                var subject = "test subject";
                var body = "Your application has been Approved";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }

                return View("Index", scholarship_details_.ToList());
            }
        }
    }
}