using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Web_Api.DAL;
using Web_Api.Models;

namespace Web_Api.Controllers
{
    
    public class InstructorsController : ApiController
    {
        private ContosoUniversityContext db = new ContosoUniversityContext();

        [HttpGet]
        [Route("~/api/instructors")]
        public IHttpActionResult GetInstructors()
        {
            return Ok(db.Instructors);
        }

        [HttpGet]
        [Route("~/api/instructors/{id}")]
        public IHttpActionResult GetInstructor(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }
        [HttpGet]
        [Route("~/api/searchInstructors")]
        public IHttpActionResult GetInstructorByKey([FromBody]String key)
        {
            var query = db.Instructors.Where(i => i.Name.Contains(key));
            if(query == null)
            {
                return NotFound();
            }
            return Ok(query);
        }

        [HttpPut]
        [Route("~/api/editInstructor/{id}")]
        public IHttpActionResult PutInstructor(int id, [FromBody]Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return BadRequest();
            }

            db.Entry(instructor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        [HttpPost]
        [Route("~/api/addInstructor")]
        public IHttpActionResult PostInstructor([FromBody]Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Instructors.Add(instructor);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("~/api/deleteInstructor/{id}")]
        public IHttpActionResult DeleteInstructor(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }
            try
            {
                db.Instructors.Remove(instructor);
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(instructor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.Instructors.Count(e => e.Id == id) > 0;
        }
    }
}