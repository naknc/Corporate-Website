using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required, StringLength(50, ErrorMessage = "Must be 50 characters!")]
        public string Email { get; set; }
        [Required, StringLength(50, ErrorMessage = "Must be 50 characters!")]
        public string Password { get; set; }
        public string Authentication { get; set; }
    }
}