using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class Chore
    {
        [Key]
        public int Id { get; set; }
        public string Chores { get; set; }
    }
}