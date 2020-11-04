using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
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
    public class UserController : ApiController
    {
        private ContosoUniversityContext db = new ContosoUniversityContext();

        [HttpPost]
        [Route("~/api/signIn")]
        public IHttpActionResult LoginUser([FromBody]User user)
        {
            var selectedUser = db.Users.FirstOrDefault(u => u.Mail.Equals(user.Mail));
            if (selectedUser != null && selectedUser.Password.Equals(Encryptor.CreateMD5(user.Password)))
            {
                var profile = new UserDTO { UserId = selectedUser.Id };
                return Ok(profile);
            }
            else
            {
                return Unauthorized();
            }
                
        }

        [HttpPost]
        [Route("~/api/signUp")]
        public IHttpActionResult createUser([FromBody]User user)
        {
            var userCompare = db.Users.FirstOrDefault(u => u.Mail.Equals(user.Mail));
            if (userCompare == null)
            {
                db.Users.Add(new User { 
                    Mail = user.Mail,
                    Password = Encryptor.CreateMD5(user.Password),
                    
                });
                db.SaveChanges();
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("~/api/users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(db.Users);
        }
    }
}
