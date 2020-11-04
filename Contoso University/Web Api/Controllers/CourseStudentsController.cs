using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Web_Api.DAL;
using Web_Api.DTO;
using Web_Api.Models;

namespace Web_Api.Controllers
{
   
    public class CourseStudentsController : ApiController
    {
        private ContosoUniversityContext db = new ContosoUniversityContext();

        [HttpGet]
        [Route("~/api/courseStudents/{id}")]
        public IHttpActionResult GetCourseStudents(int id)
        {
            var query = db.Courses.Where(c => c.Id == id).SelectMany(
                c => c.Students.Select(
                s => new CourseStudentDTO
                {
                    CourseId = c.Id,
                    CourseTitle = c.Title,
                    StudentId = s.Id,
                    StudentFullName = s.LastName + " " + s.Name
                }));
            var list = query.ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("~/api/studentCourses/{id}")]
        public IHttpActionResult GetStudentCourses(int id)
        {
            var query = db.Students.Where(s => s.Id == id).SelectMany(
                s => s.Courses.Select(
                c => new CourseStudentDTO
                {
                    CourseId = c.Id,
                    CourseTitle = c.Title,
                    StudentId = s.Id,
                    StudentFullName = s.LastName + " " + s.Name
                }
                ));
            var list = query.ToList();
            return Ok(list);
        }
        /*
        [HttpGet]
        [Route("~/api/enrollments")]
        public IHttpActionResult getEnrollments()
        {
            return Ok(db.CourseStudents);
        }

        [HttpPost]
        [Route("~/api/enroll/{cid}/{sid}")]
        public IHttpActionResult enrollStudent(int cid, int sid)
        {
            var student = db.Students.Find(sid);
            var course = db.Courses.Find(cid);
            if(student != null && course != null)
            {
                db.CourseStudents.Add(new CourseStudent
                {
                    Student = student,
                    Course = course,
                    State = State.StandBy
                }) ;
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("~/api/deleteEnrollment/{id}")]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            CourseStudent courseStudent = db.CourseStudents.Find(id);
            if (courseStudent == null)
            {
                return NotFound();
            }

            db.CourseStudents.Remove(courseStudent);
            db.SaveChanges();

            return Ok(courseStudent);
        }

        [HttpPut]
        [Route("~/aceptEnrollment/{id}")]
        public IHttpActionResult aceptEnrollment(int id)
        {
            var enrollment = db.CourseStudents.Find(id);
            if(enrollment == null)
            {
                return NotFound();
            }
            enrollment.State = State.Approved;

            db.Entry(enrollment).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("~/rejectEnrollment/{id}")]
        public IHttpActionResult rejectEnrollment(int id)
        {
            var enrollment = db.CourseStudents.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            enrollment.State = State.Rejected;

            db.Entry(enrollment).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
        */
    }
}
