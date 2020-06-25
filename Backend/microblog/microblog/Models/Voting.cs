using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace microblog.Models
{
    public class Voting
    {
        public bool Status { get; set; }

        public int VotingID { get; set; }

        public int UserID { get; set; }

        public int TweetID { get; set; }
    }
}