using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WestBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
        [Required]
        [StringLength(750, ErrorMessage = "The maximum length is 750 characters.", MinimumLength =1)]
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        [Display(Name ="Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name ="Original Comment")]
        public string OriginalBody { get; set; }
        public string PreviousBody { get; set; }
        public bool Published { get; set; }
        [StringLength(500, ErrorMessage = "You must enter a reason to remove this comment.", MinimumLength =5)]
        public string ReasonRemoved { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Post Post { get; set; }
    }
}