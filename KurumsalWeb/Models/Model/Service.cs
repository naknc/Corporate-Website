using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace KurumsalWeb.Models.Model
{
   [Table("Service")]
    public class Service
    {
        [Key]
        public int HizmetId { get; set; }
        [Required, StringLength(150, ErrorMessage = "Must be 150 characters!")]
        [DisplayName("Service Title")]
        public string Title { get; set; }
        [DisplayName("Service Description")]
        public string Description { get; set; }
        [DisplayName("Service Image")]
        public string ImageURL { get; set; }
    }
}