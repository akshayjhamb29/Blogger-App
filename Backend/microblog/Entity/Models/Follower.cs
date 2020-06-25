using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
   public class Following
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int FollowingID { get; set; }
        public User Follow { get; set; }
    }
}
