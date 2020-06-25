using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public class Tweet
    {
        public int TweetID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Address { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        //public virtual ICollection<Voting> Votings { get; set; }
    }
}
