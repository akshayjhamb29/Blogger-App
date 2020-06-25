using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Voting
    {
        public bool Status { get; set; }

        public int VotingID { get; set; }

        public int UserID { get; set; }
        //public virtual User User { get; set; }

        public int TweetID { get; set; }
        //public virtual Tweet Tweet { get; set; }

    }
}
