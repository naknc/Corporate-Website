using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(50, ErrorMessage = "Must be 50 characters!")]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}