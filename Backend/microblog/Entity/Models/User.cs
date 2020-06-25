using System.Collections.Generic;

namespace Entity.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string EmailID  { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Tweet> Tweets { get; set; }
        //public virtual ICollection<Voting> Votings { get; set; }
        public virtual ICollection<Following> Followings { get; set; }

    }
}
