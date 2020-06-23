using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("")]
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [StringLength(250, ErrorMessage = "Must be 250 characters!")]
        public string Address { get; set; }
        [StringLength(250, ErrorMessage = "Must be 250 characters!")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
    }
}