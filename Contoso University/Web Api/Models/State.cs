using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api.Models
{
    public class State
    {
        const String standBy = "Stand By";
        const String approved = "Aproved";
        const String rejected = "Rejected";

        public static String StandBy => standBy;
        public static String Approved => approved;
        public static String Rejected => rejected;
    }
}