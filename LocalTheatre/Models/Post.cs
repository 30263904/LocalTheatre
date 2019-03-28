using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalTheatre.Models
{
    public class Post
    {
        //attributes
        [Key]
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Comment> CommentList { get; set; }

        //my default constructor
        public Post()
        {
            Date = DateTime.Now;
            CommentList = new List<Comment>();
        }

        //my overloaded constructor
        public Post(int categoryId, string userId, string title, string text)
        {
            CategoryId = categoryId;
            UserId = userId;
            Date = DateTime.Now;
            Title = title;
            Text = text;
            CommentList = new List<Comment>();
        }
    }
}