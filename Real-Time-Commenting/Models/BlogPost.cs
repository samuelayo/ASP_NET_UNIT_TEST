using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Real_Time_Commenting.Models
{
    public class BlogPost
    {
        public int BlogPostID { get; set; }


        public string Title { get; set; }
        [Column(TypeName = "ntext")]
        public string Body { get; set; }

    }
}