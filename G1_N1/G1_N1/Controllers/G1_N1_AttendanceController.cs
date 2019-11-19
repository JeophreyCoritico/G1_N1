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
    public class G1_N1_AttendanceController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_Attendance
        public IQueryable<G1_N1_Attendance> GetG1_N1_Attendance()
        {
            return db.G1_N1_Attendance;
        }

        // GET: api/G1_N1_Attendance/5
        [ResponseType(typeof(G1_N1_Attendance))]
        public IHttpActionResult GetG1_N1_Attendance(DateTime id)
        {
            G1_N1_Attendance g1_N1_Attendance = db.G1_N1_Attendance.Find(id);
            if (g1_N1_Attendance == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_Attendance);
        }

        // PUT: api/G1_N1_Attendance/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_Attendance(DateTime id, G1_N1_Attendance g1_N1_Attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_Attendance.SignIn)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_Attendance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_AttendanceExists(id))
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

        // POST: api/G1_N1_Attendance
        [ResponseType(typeof(G1_N1_Attendance))]
        public IHttpActionResult PostG1_N1_Attendance(G1_N1_Attendance g1_N1_Attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_Attendance.Add(g1_N1_Attendance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_AttendanceExists(g1_N1_Attendance.SignIn))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_Attendance.SignIn }, g1_N1_Attendance);
        }

        // DELETE: api/G1_N1_Attendance/5
        [ResponseType(typeof(G1_N1_Attendance))]
        public IHttpActionResult DeleteG1_N1_Attendance(DateTime id)
        {
            G1_N1_Attendance g1_N1_Attendance = db.G1_N1_Attendance.Find(id);
            if (g1_N1_Attendance == null)
            {
                return NotFound();
            }

            db.G1_N1_Attendance.Remove(g1_N1_Attendance);
            db.SaveChanges();

            return Ok(g1_N1_Attendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_AttendanceExists(DateTime id)
        {
            return db.G1_N1_Attendance.Count(e => e.SignIn == id) > 0;
        }
    }
}