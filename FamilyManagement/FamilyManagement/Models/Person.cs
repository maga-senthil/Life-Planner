using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Adult { get; set; }
        public bool Child { get; set; }
        public bool Nanny { get; set; }
        [ForeignKey("ApplicationUsers")]
        public string LoginId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
    }
}