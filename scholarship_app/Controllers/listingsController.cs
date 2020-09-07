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
    public class listingsController : Controller
    {
        private Entities db = new Entities();

        // GET: listings
        public async Task<ActionResult> Index()
        {
            var listings = db.listings.Include(l => l.student);
            return View(await listings.ToListAsync());
        }

        // GET: listings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listing listing = await db.listings.FindAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // GET: listings/Create
        public ActionResult Create()
        {
            ViewBag.student_id = new SelectList(db.students, "Id", "Name");
            return View();
        }

        // POST: listings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( listing listing)
        {
            if (ModelState.IsValid)
            {
                var l = new listing();
                l.Amount = listing.Amount;
                l.CreatedTime = DateTime.Now;
                l.Currency = listing.Currency;
                l.EndDate = listing.EndDate;
                l.GivenBy = listing.GivenBy;
                l.NoAvailable = listing.NoAvailable;
                var str = System.Web.HttpContext.Current.Session["UserName"].ToString();
                l.student_id =  db.students.Where(x=>x.Name == str  ).FirstOrDefault().Id;

                db.listings.Add(l);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "Id", "Name", listing.student_id);
            return View(listing);
        }

        // GET: listings/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listing listing = await db.listings.FindAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.student_id = new SelectList(db.students, "Id", "Name", listing.student_id);
            return View(listing);
        }

        // POST: listings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GivenBy,CreatedTime,Amount,Currency,EndDate,NoAvailable,student_id")] listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listing).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.student_id = new SelectList(db.students, "Id", "Name", listing.student_id);
            return View(listing);
        }

        // GET: listings/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listing listing = await db.listings.FindAsync(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // POST: listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            listing listing = await db.listings.FindAsync(id);
            db.listings.Remove(listing);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
