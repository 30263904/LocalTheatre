using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalTheatre.Models
{
    public class Comment
    {
        //the attributes of a comment listed below
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
       
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        
        //default constructor listed below
        public Comment()
        {
            Date = DateTime.Now;
        }

        //the overloaded constructor
        public Comment(int commentId, int postId, string userId, string text)
        {
            CommentId = commentId;
            PostId = postId;
            UserId = userId;
            Date = DateTime.Now;
            Text = text;
        }
    }

}