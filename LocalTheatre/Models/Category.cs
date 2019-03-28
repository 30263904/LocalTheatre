using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalTheatre.Models
{
    public class Category
    {
        //attributes 
        [Key]
        public int CategoryId { get; set; }
        public string Type { get; set; }

        //default constructor
        public Category()
        {
        }

        //overloaded constructor
        public Category(int categoryId, string type)
        {
            CategoryId = categoryId;

            Type = type;

        }
    }

}