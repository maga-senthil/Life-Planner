using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Start")]
        public string start { get; set; }
        [Display(Name = "End")]
        public string end { get; set; }
    
    }
}