using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class DailyChore
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Family")]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        [ForeignKey("Chore")]
        public int ChoreId { get; set; }
        public Chore Chore { get; set; }
        public DateTime ChoreDay { get; set; }
        public bool Done { get; set; }
    }
}