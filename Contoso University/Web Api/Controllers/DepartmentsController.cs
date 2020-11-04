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

    public class DepartmentsController : ApiController
    {
        private ContosoUniversityContext db = new ContosoUniversityContext();

        [HttpGet]
        [Route("~/api/departments")]
        public IHttpActionResult GetDepartments()
        {
            return Ok(db.Departments);
        }

        [HttpGet]
        [Route("~/api/departments/{id}")]
        public IHttpActionResult GetDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
        [HttpGet]
        [Route("~/api/searchDepartments")]
        public IHttpActionResult GetDepartmentsByKey([FromBody]String key)
        {
            var query = db.Departments.Where(c => c.Title.Contains(key));
            if(query == null)
            {
                return NotFound();
            }
            return Ok(query);
        }

        [HttpPut]
        [Route("~/api/editDepartment/{id}")]
        public IHttpActionResult PutDepartment(int id, [FromBody]Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
        [Route("~/api/addDepartment")]
        public IHttpActionResult PostDepartment([FromBody]Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("~/api/deleteDepartment/{id}")]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.Id == id) > 0;
        }
    }
}