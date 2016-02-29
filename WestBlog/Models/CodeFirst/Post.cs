using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WestBlog.Models
{  //If organizing Models with a subfolder, be sure to delete that subfolder from the namespace
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>(); //instantiates the Comments collection for use; 'this' points to the particular instance object Post
        }
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; } //? allows null. Can also use <nullable> before variable name
        [Required]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
        public string MediaURL { get; set; }
        public string Category { get; set; }
        public bool Published { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }  //setting navigational properties allows us to access post comments through the post class
                                                //naming convention to make collection variables plural
    }
}