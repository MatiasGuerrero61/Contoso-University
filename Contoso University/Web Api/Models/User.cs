using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api.Models
{
    public class User
    {
        private int id;
        private string mail;
        private string password;

        public int Id { get => id; set => id = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
    }
}