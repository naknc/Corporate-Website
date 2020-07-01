using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]
    public class Slider
    {        
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Slider Title"), StringLength(30, ErrorMessage = "Must be 30 characters!")]
        public string Title { get; set; }
        [DisplayName("Slider Description"), StringLength(150, ErrorMessage = "Must be 150 characters!")]
        public string Description { get; set; }
        [DisplayName("Slider Image"), StringLength(250)]
        public string ImageURL { get; set; }
    }
}