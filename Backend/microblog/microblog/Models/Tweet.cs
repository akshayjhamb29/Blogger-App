using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace microblog.Models
{
    public class Tweet
    {
        public int TweetID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Address { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}