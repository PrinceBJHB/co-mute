using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CoMute.DAL;
using CoMute.Models;

namespace CoMute.WebApi.Controllers
{
    public class CarPoolsController : ApiController
    {
        private CarPoolContext db = new CarPoolContext();

        // GET: api/CarPools
        public IQueryable<CarPool> GetCarPools()
        {
            return db.CarPools;
        }

        // GET: api/CarPools/5
        [ResponseType(typeof(CarPool))]
        public IHttpActionResult GetCarPool(Guid id)
        {
            CarPool carPool = db.CarPools.Find(id);
            if (carPool == null)
            {
                return NotFound();
            }

            return Ok(carPool);
        }

        // PUT: api/CarPools/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarPool(Guid id, CarPool carPool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carPool.CarPoolID)
            {
                return BadRequest();
            }

            db.Entry(carPool).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarPoolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CarPools
        [ResponseType(typeof(CarPool))]
        public IHttpActionResult PostCarPool(CarPool carPool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarPools.Add(carPool);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CarPoolExists(carPool.CarPoolID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = carPool.CarPoolID }, carPool);
        }

        // DELETE: api/CarPools/5
        [ResponseType(typeof(CarPool))]
        public IHttpActionResult DeleteCarPool(Guid id)
        {
            CarPool carPool = db.CarPools.Find(id);
            if (carPool == null)
            {
                return NotFound();
            }

            db.CarPools.Remove(carPool);
            db.SaveChanges();

            return Ok(carPool);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarPoolExists(Guid id)
        {
            return db.CarPools.Count(e => e.CarPoolID == id) > 0;
        }
    }
}