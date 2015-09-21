using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models;
using System.IO;

namespace CoMute.Web.Controllers
{
    public class Car_poolController : Controller
    {
        private CoMuteWebContext db = new CoMuteWebContext();

        // GET: Car_pool
        public async Task<ActionResult> Index()
        {
            return View(await db.Car_pool.ToListAsync());
        }

        // GET: Car_pool/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_pool car_pool = await db.Car_pool.FindAsync(id);
            if (car_pool == null)
            {
                return HttpNotFound();
            }
            return View(car_pool);
        }

        // GET: Car_pool/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car_pool/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DepartureTime,ArrivalTime,Origin,DayAvailable,Destination,Owner,Notes")] Car_pool car_pool)
        {
            
            var result = db.Car_pool.FirstOrDefault(s => s.Id == 1);
            var departure = result.DepartureTime;
            var arrival = result.ArrivalTime;

            if(car_pool.DepartureTime > departure && car_pool.ArrivalTime < arrival)
            {
                TempData["Message"] = "The car pool overlaps!";
                return RedirectToAction("Index", "Car_pool");
            }
            else { 
            if (ModelState.IsValid)
            {
                db.Car_pool.Add(car_pool);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(car_pool);
                }
        }
        // Search: Car_pool/
        public async Task<ActionResult> Search(string Destination)
        {
            if (Destination == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_pool car_pool = await db.Car_pool.FindAsync(Destination);
            if (car_pool == null)
            {
                return HttpNotFound();
            }
            return View(car_pool);
        }

        // GET: Car_pool/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_pool car_pool = await db.Car_pool.FindAsync(id);
            if (car_pool == null)
            {
                return HttpNotFound();
            }
            return View(car_pool);
        }

        // POST: Car_pool/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DepartureTime,ArrivalTime,Origin,DayAvailable,Destination,Owner,Notes")] Car_pool car_pool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car_pool).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(car_pool);
        }

        // GET: Car_pool/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_pool car_pool = await db.Car_pool.FindAsync(id);
            if (car_pool == null)
            {
                return HttpNotFound();
            }
            return View(car_pool);
        }

        // POST: Car_pool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Car_pool car_pool = await db.Car_pool.FindAsync(id);
            db.Car_pool.Remove(car_pool);
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
