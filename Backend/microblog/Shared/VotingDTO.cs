﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class VotingDTO
    {
        public bool Status { get; set; }

        public int VotingID { get; set; }

        public int UserID { get; set; }

        public int TweetID { get; set; }
    }
}