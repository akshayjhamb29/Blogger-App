using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
   public class UserDTO
    {
        public int UserID { get; set; }
        public string EmailID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        //public List<TweetDTO> Tweets { get; set; }
        //public List<FollowerDTO> Followings { get; set; }
    }
}
