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
using G1_N1.Models;

namespace G1_N1.Controllers
{
    public class G1_N1_roomController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_room
        public IQueryable<G1_N1_room> GetG1_N1_room()
        {
            return db.G1_N1_room;
        }

        // GET: api/G1_N1_room/5
        [ResponseType(typeof(G1_N1_room))]
        public IHttpActionResult GetG1_N1_room(string id)
        {
            G1_N1_room g1_N1_room = db.G1_N1_room.Find(id);
            if (g1_N1_room == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_room);
        }

        // PUT: api/G1_N1_room/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_room(string id, G1_N1_room g1_N1_room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_room.RoomNo)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_roomExists(id))
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

        // POST: api/G1_N1_room
        [ResponseType(typeof(G1_N1_room))]
        public IHttpActionResult PostG1_N1_room(G1_N1_room g1_N1_room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_room.Add(g1_N1_room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_roomExists(g1_N1_room.RoomNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_room.RoomNo }, g1_N1_room);
        }

        // DELETE: api/G1_N1_room/5
        [ResponseType(typeof(G1_N1_room))]
        public IHttpActionResult DeleteG1_N1_room(string id)
        {
            G1_N1_room g1_N1_room = db.G1_N1_room.Find(id);
            if (g1_N1_room == null)
            {
                return NotFound();
            }

            db.G1_N1_room.Remove(g1_N1_room);
            db.SaveChanges();

            return Ok(g1_N1_room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_roomExists(string id)
        {
            return db.G1_N1_room.Count(e => e.RoomNo == id) > 0;
        }
    }
}