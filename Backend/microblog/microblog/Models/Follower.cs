using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microblog.Models
{
    public class Follower
    {
        public int UserID { get; set; }
        public int FollowerID { get; set; }
    }
}