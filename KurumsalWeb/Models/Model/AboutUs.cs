using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("AboutUs")]
    public class AboutUs
    {
        [Key]
        public int AboutUsId { get; set; }
        [Required]
        [DisplayName("Description About Us")]
        public string Description { get; set; }

    }
}