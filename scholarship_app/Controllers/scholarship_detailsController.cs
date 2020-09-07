using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using scholarship_app.Models;

namespace scholarship_app.Controllers
{
    
    public class scholarship_detailsController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Scholarshipsite()
        {

            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Main()
        {
            try
            {
                var name = System.Web.HttpContext.Current.Session["UserName"].ToString();
                var listings = db.listings.Where(x => x.student.Name == name).Select(x => x);
                return View(db.listings.ToList().OrderBy(x=>x.EndDate).ToList());
            }
            catch
            {
                return RedirectToAction("Index", "StudentLogin");

            }
         

        }
            
            
        // GET: scholarship_details
        public async Task<ActionResult> Index()
        {
            var student_id = System.Web.HttpContext.Current.Session["UserName"].ToString();
            var scholarship_details = db.scholarship_details.Include(s => s.student).Where(x=>x.student.Name == student_id);
            return View(await scholarship_details.ToListAsync());
        }

        // GET: scholarship_details/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scholarship_details scholarship_details = await db.scholarship_details.FindAsync(id);
            if (scholarship_details == null)
            {
                return HttpNotFound();
            }
            return View(scholarship_details);
        }

        // GET: scholarship_details/Create
      
        public ActionResult Create(long id)
        {
            ViewBag.student_id = new SelectList(db.students, "Id", "Name");

            Session["ApplyId"] = id;

            return View();
        }

        // POST: scholarship_details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( scholarship_details scholarship_details)
        {
            if (ModelState.IsValid)
            {
                var student_id = System.Web.HttpContext.Current.Session["UserName"].ToString(); 
                var i = db.scholarship_details.Where(x => x.student.Name == student_id).FirstOrDefault();
                var id = db.students.Where(x => x.Name == student_id).FirstOrDefault().Id;
                if( i != null)
                {
                    ModelState.AddModelError("", "Already applied for scholarship");

                    return RedirectToAction("Index");
                }
                var sc = new scholarship_details();
                sc.amount = scholarship_details.amount;
                sc.reason = scholarship_details.reason;
                sc.address = scholarship_details.address;
                sc.student_id = id;
                sc.status = "Pending";
                db.scholarship_details.Add(sc);

                var apply_id = System.Web.HttpContext.Current.Session["ApplyId"].ToString();

                availed av = new availed();
                av.listing_id = Convert.ToInt64(apply_id);
                av.student_id = id;
                db.availeds.Add(av);


                await db.SaveChangesAsync();





                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "Id", "Name", scholarship_details.student_id);
            return View(scholarship_details);
        }

        // GET: scholarship_details/Edit/5
        //public async Task<ActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    scholarship_details scholarship_details = await db.scholarship_details.FindAsync(id);
        //    if (scholarship_details == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.student_id = new SelectList(db.students, "Id", "Name", scholarship_details.student_id);
        //    return View(scholarship_details);
        //}

        // POST: scholarship_details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,student_id,amount,address,reason,status")] scholarship_details scholarship_details)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(scholarship_details).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.student_id = new SelectList(db.students, "Id", "Name", scholarship_details.student_id);
        //    return View(scholarship_details);
        //}

        // GET: scholarship_details/Delete/5
        //public async Task<ActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    scholarship_details scholarship_details = await db.scholarship_details.FindAsync(id);
        //    if (scholarship_details == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(scholarship_details);
        //}

        //// POST: scholarship_details/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(long id)
        //{
        //    scholarship_details scholarship_details = await db.scholarship_details.FindAsync(id);
        //    db.scholarship_details.Remove(scholarship_details);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
