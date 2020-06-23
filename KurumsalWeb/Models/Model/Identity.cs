using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Identity")]
    public class Identity
    {
        [Key]
        public int IdentityId { get; set; }
        [DisplayName("Site Title")]
        [Required, StringLength(100, ErrorMessage = "Must be 100 characters!")]
        public string Title { get; set; }
        [DisplayName("Keywords")]
        [Required, StringLength(200, ErrorMessage = "Must be 200 characters!")]
        public string Keywords { get; set; }
        [DisplayName("Site Description")]
        [Required, StringLength(300, ErrorMessage = "Must be 300 characters!")]
        public string Description { get; set; }
        [DisplayName("Site Logo")]
        public string LogoURL { get; set; }
        [DisplayName("Site Degree")]
        public string Degree { get; set; }
    }
}