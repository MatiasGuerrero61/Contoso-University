using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Web_Api.DAL;
using Web_Api.DTO;
using Web_Api.Models;

namespace Web_Api.Controllers
{
    public class CoursesController : ApiController
    {
        private ContosoUniversityContext db = new ContosoUniversityContext();

       [HttpGet]
       [Route("~/api/courses")]
        public IHttpActionResult GetCourses()
        {
                var query = db.Courses.Include(u => u.Department).Include(v => v.Instructor).Select(
                    m => new CourseDTO
                    {
                        CourseId = m.Id,
                        CourseName = m.Title,
                        DepartmentName = m.Department.Title,
                        InstructorName = m.Instructor.Name +" "+ m.Instructor.LastName
                    }
                    );
                return Ok(query);
        }
        [HttpGet]
        [Route("~/api/searchCourses")]
        public IHttpActionResult GetCoursesByKey([FromBody]String key)
        {
            var query = db.Courses.Where(c =>c.Title.Contains(key)).Include(u => u.Department).Include(v => v.Instructor).Select(
                    m => new CourseDTO
                    {
                        CourseId = m.Id,
                        CourseName = m.Title,
                        DepartmentName = m.Department.Title,
                        InstructorName = m.Instructor.Name + " " + m.Instructor.LastName
                    }
                    );
            return Ok(query);
        }

        [HttpGet]
        [Route("~/api/searchCourses/{id}")]
        public IHttpActionResult getCoursesByDepartment(int id)
        {
            var department = db.Departments.Find(id);
            if(department == null)
            {
                return NotFound();
            }

            var query = db.Courses.Where(c=> c.DepartmentId == id && c.Department.Equals(department))
                .Include(u => u.Department).Include(v => v.Instructor).Select(
                    m => new CourseDTO
                    {
                        CourseId = m.Id,
                        CourseName = m.Title,
                        DepartmentName = m.Department.Title,
                        InstructorName = m.Instructor.Name + " " + m.Instructor.LastName
                    }
                    );
            return Ok(query);
        }

        [HttpGet]
        [Route("~/api/courses/{id}")]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPut]
        [Route("~/api/editCourse/{id}")]
        public IHttpActionResult PutCourse(int id, [FromBody]Course course)
        {

            if (id != course.Id)
            {
                return BadRequest();
            }
            Department department = db.Departments.Find(course.DepartmentId);
            Instructor instructor = db.Instructors.Find(course.InstructorId);

            if (department == null || instructor == null)
            {
                return BadRequest();
            }
            course.Instructor = instructor;
            course.Department = department;

            try
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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
        [Route("~/api/addCourse")]
        public IHttpActionResult PostCourse([FromBody]Course course)
        {
            var department = db.Departments.Find(course.DepartmentId);
            var instructor = db.Instructors.Find(course.InstructorId);
            if (department != null && instructor != null)
            {
                course.Department = department;
                course.Instructor = instructor;

                db.Courses.Add(course);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("~/api/deleteCourse/{id}")]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.Id == id) > 0;
        }
    }
}