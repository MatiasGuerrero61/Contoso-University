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
    
    public class StudentsController : ApiController
    {
        private ContosoUniversityContext db = new ContosoUniversityContext();

        [HttpGet]
        [Route("~/api/students")]
        public IHttpActionResult GetStudents()
        {
            return Ok(db.Students);
        }

        [HttpGet]
        [Route("~/api/students/{id}")]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPut]
        [Route("~/api/editStudent/{id}")]
        public IHttpActionResult PutStudent(int id, [FromBody]Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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
        [Route("~/api/addStudent")]
        public IHttpActionResult PostStudent ([FromBody]Student student)
        {
            db.Students.Add(new Student { 
                 Name = student.Name,
                 LastName = student.LastName,
                 Dni = student.Dni,
                // UserId = id
            });
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("~/api/deleteStudent/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        [HttpGet]
        [Route("~/api/userStudent/{id}")]
        public IHttpActionResult GetUserStudent(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.UserId.Equals(id));
            if(student != null)
            {
                return Ok(student);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("~/api/enroll/{id}")]
        public IHttpActionResult enrollStudent(int id, [FromBody]Student student)
        {
            try
            {
                var enrollingStudent = db.Students.FirstOrDefault(s => s.Id == student.Id);
                var course = db.Courses.FirstOrDefault(c => c.Id == id);
                course.Students.Add(enrollingStudent);
                enrollingStudent.Courses.Add(course);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.Id == id) > 0;
        }
    }
}